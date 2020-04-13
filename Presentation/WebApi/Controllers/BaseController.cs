using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GovHospitalApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public readonly ILogger _logger;

        public readonly IMediator _mediator;

        public BaseController(IMediator mediator, ILogger logger)
        {
            _logger = logger ??
            throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        }
    }
}
