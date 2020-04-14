using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public readonly ILogger Logger;

        public readonly IMediator Mediator;

        protected BaseController(IMediator mediator, ILogger logger)
        {
            Logger = logger ??
                      throw new ArgumentNullException(nameof(logger));
            Mediator = mediator ??
                        throw new ArgumentNullException(nameof(mediator));
        }
    }
}