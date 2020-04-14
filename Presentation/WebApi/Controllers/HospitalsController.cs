using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Hospitals.Commands;
using Application.Hospitals.Models;
using Application.Hospitals.Queries;
using Application.Patients.Models;
using Application.Patients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    public class HospitalsController : BaseController
    {
        public HospitalsController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Hospital>> GetAllHospitalAsync()
        {
            var query = new GetHospitals.Query();
            return await Mediator.Send(query);
        }

        [HttpGet("{hospitalId}")]
        public async Task<Hospital> GetHospitalByIdAsync(Guid hospitalId)
        {
            var query = new GetHospital.Query(hospitalId);
            return await Mediator.Send(query);
        }

        [HttpGet("{hospitalId}/Patients")]
        public async Task<IEnumerable<Patient>> GetPatientsByHospitalIdAsync(Guid hospitalId)
        {
            var query = new GetPatientsByHospitalId.Query(hospitalId);
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Guid> SaveHospitalAsync([FromBody] SaveHospital.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("{hospitalId}")]
        public async Task<ActionResult<Guid>> EditHospitalAsync([FromRoute] Guid hospitalId,
            [FromBody] EditHospital.Command command)
        {
            if (hospitalId == command.Id) return await Mediator.Send(command);

            ModelState.AddModelError(nameof(command.Id), "Id should be match");
            return BadRequest();

        }
    }
}