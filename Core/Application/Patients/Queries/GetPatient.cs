using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Patients.Models;
using MediatR;

namespace Application.Patients.Queries
{
    public sealed class GetPatient
    {
        public sealed class Query : IRequest<Patient>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public sealed class Handler : IRequestHandler<Query, Patient>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                   throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<Patient> Handle(Query request, CancellationToken cancellationToken)
            {
                var patient = await _appDbRepository.GetPatientByIdAsync(request.Id);
                if (patient == null) throw new NotFoundException(nameof(Patient), request.Id);

                return ToPatientViewModel(patient);
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