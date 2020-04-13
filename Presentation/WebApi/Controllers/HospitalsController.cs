using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Commands;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Models;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Queries;
using GovHospitalApp.Core.Application.Infrastructure.Patients.Queries;
using GovHospitalApp.Core.Application.Patients.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovHospitalApp.Controllers
{
    public class HospitalsController : BaseController
    {
        public HospitalsController(IMediator mediator, ILogger<HospitalsController> logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Hospital>> GetAllHospitalAsync()
        {
            var query = new GetHospitals.Query();
            return await _mediator.Send(query);
        }

        [HttpGet("{hospitalId}")]
        public async Task<Hospital> GetHospitalByIdAsync(Guid hospitalId)
        {
            var query = new GetHospital.Query(hospitalId);
            return await _mediator.Send(query);
        }

        [HttpGet("{hospitalId}/Patients")]
        public async Task<IEnumerable<Patient>> GetPatientsByHospitalIdAsync(Guid hospitalId)
        {
            var query = new GetPatientsByHospitalId.Query(hospitalId);
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<Guid> SaveHospitalAsync([FromBody] SaveHospital.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("{hospitalId}")]
        public async Task<ActionResult<Guid>> EditHospitalAsync([FromRoute] Guid hospitalId, [FromBody] EditHospital.Command command)
        {
            if (hospitalId != command.Id)
            {
                ModelState.AddModelError(nameof(command.Id), "Id should be match");
                return BadRequest();
            }
            return await _mediator.Send(command);
        }
    }
}
