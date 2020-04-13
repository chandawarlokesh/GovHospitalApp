using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Infrastructure
{
    public class RequestPerformaceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<RequestPerformaceBehavior<TRequest, TResponse>> _logger;

        public RequestPerformaceBehavior(ILogger<RequestPerformaceBehavior<TRequest, TResponse>> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                _logger.LogInformation($"Long running Request ({_timer.ElapsedMilliseconds}ms)- {name}: {request}");
            }

            return response;
        }
    }
}
