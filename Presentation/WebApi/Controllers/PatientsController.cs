using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Patients.Commands;
using Application.Patients.Models;
using Application.Patients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    public class PatientsController : BaseController
    {
        public PatientsController(IMediator mediator, ILogger logger) : base(mediator, logger)
        {
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<IEnumerable<Patient>> GetAllPatientAsync()
        {
            var query = new GetPatients.Query();
            return await Mediator.Send(query);
        }

        [HttpGet("login/{mobileNumber}")]
        public async Task<Guid> Login([FromRoute] string mobileNumber)
        {
            var query = new GetPatientByMobileNumber.Query(mobileNumber);
            return await Mediator.Send(query);
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<Patient>> GetPatientByIdAsync(Guid patientId)
        {
            if (patientId == default)
            {
                ModelState.AddModelError(nameof(patientId), "Patient Id should not be null or default");
                return BadRequest(ModelState);
            }

            var query = new GetPatient.Query(patientId);
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Guid> SavePatientAsync([FromBody] SavePatient.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Guid>> EditPatientAsync([FromRoute] Guid id,
            [FromBody] EditPatient.Command command)
        {
            if (id == command.Id) return await Mediator.Send(command);
            ModelState.AddModelError(nameof(id), "Id should be match");
            return BadRequest();
        }
    }
}