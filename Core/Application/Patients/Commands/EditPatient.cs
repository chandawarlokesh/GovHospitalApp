using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Notifications.Models;
using Application.Patients.Models;
using Domain.Enumerations;
using MediatR;
using Newtonsoft.Json;

namespace Application.Patients.Commands
{
    public sealed class EditPatient
    {
        public sealed class Command : IRequest<Guid>
        {
            [JsonConstructor]
            public Command(Guid id, string name, DateTime dateOfBirth, int gender, Address address, string mobileNumber,
                Guid? hospitalId)
            {
                Id = id;
                Name = name;
                DateOfBirth = dateOfBirth;
                Gender = gender;
                Address = address;
                MobileNumber = mobileNumber;
                HospitalId = hospitalId;
            }

            public Guid Id { get; }
            public string Name { get; }
            public DateTime DateOfBirth { get; }
            public int Gender { get; }
            public Address Address { get; }
            public string MobileNumber { get; }
            public Guid? HospitalId { get; }
        }

        public sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IAppDbRepository _appDbRepository;
            private readonly INotificationService _notificationService;

            public Handler(IAppDbRepository appDbRepository, INotificationService notificationService)
            {
                _appDbRepository = appDbRepository ??
                                   throw new ArgumentNullException(nameof(appDbRepository));
                _notificationService = notificationService ??
                                       throw new ArgumentNullException(nameof(notificationService));
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingPatient = await _appDbRepository.GetPatientByIdAsync(request.Id);
                if (existingPatient == null) throw new NotFoundException(nameof(Patient), request.Id);

                var patientHospitalId = request.HospitalId;
                if (request.HospitalId != null)
                {
                    var hospital = await _appDbRepository.GetHospitalByIdAsync(request.HospitalId.Value);

                    if (hospital == null) throw new NotFoundException(nameof(Patient), request.HospitalId);
                    patientHospitalId = hospital.HospitalId;
                }

                var patient = new Domain.Entities.Patient
                {
                    PatientId = request.Id,
                    Name = request.Name,
                    DateOfBirth = request.DateOfBirth,
                    Gender = (GenderType) request.Gender,
                    MobileNumber = request.MobileNumber,
                    HospitalId = patientHospitalId,
                    Address = new Domain.Entities.Address
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        State = request.Address.State,
                        ZipCode = request.Address.ZipCode
                    }
                };
                await _appDbRepository.EditPatientByIdAsync(request.Id, patient);
                _notificationService.Send(new Message
                {
                    Text = $"Patient updated with the id {request.Id}",
                    Payload = patient
                });
                return request.Id;
            }
        }
    }
}