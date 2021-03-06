using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Patients.Models;
using MediatR;

namespace Application.Patients.Queries
{
    public sealed class GetPatientsByHospitalId
    {
        public sealed class Query : IRequest<IEnumerable<Patient>>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
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
                var patients = await _appDbRepository.GetPatientsByHospitalIdAsync(request.Id);

                return patients?.Select(ToPatientViewModel);
            }

            private static Patient ToPatientViewModel(Domain.Entities.Patient patient)
            {
                var patientAddress = new Address(patient.Address.Street, patient.Address.City,
                    patient.Address.State, patient.Address.ZipCode);

                return new Patient(patient.PatientId, patient.Name, patient.DateOfBirth,
                    patient.Gender, patientAddress, patient.MobileNumber, patient.HospitalId);
            }
        }
    }
}