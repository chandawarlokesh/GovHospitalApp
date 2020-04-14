using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Application.Patients.Queries
{
    public sealed class GetPatientByMobileNumber
    {
        public sealed class Query : IRequest<Guid>
        {
            [JsonConstructor]
            public Query(string mobileNumber)
            {
                MobileNumber = mobileNumber;
            }

            public string MobileNumber { get; set; }
        }

        public sealed class Handler : IRequestHandler<Query, Guid>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                   throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<Guid> Handle(Query request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(request.MobileNumber)) throw new ArgumentNullException();

                var patient = await _appDbRepository.GetPatientIdByMobileNumberAsync(request.MobileNumber);
                if (patient == null) throw new UnauthorizedAccessException();
                return patient.PatientId;
            }
        }
    }
}