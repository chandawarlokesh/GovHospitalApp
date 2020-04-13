using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Application.Patients.Models;
using GovHospitalApp.Core.Domain.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Infrastructure.Patients.Queries
{
    public sealed class GetPatients
    {
        public sealed class Query : IRequest<IEnumerable<Patient>>
        {
        }

        public sealed class Handler : IRequestHandler<Query, IEnumerable<Patient>>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                            throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<IEnumerable<Patient>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patients = await _appDbRepository.GetPatientsAsync();


                return patients?.Select(ToPatientViewModel);
            }

            private static Patient ToPatientViewModel(Domain.Entities.Patient patient)
            {
                var patientAddress = new Address(patient.Address.Street, patient.Address.City,
                    patient.Address.State, patient.Address.ZipCode);

                return new Patient(patient.PatientId, patient.Name, patient.DateOfBirth,
                    (GenderType)patient.Gender, patientAddress, patient.MobileNumber, patient.HospitalId);
            }
        }
    }
}
