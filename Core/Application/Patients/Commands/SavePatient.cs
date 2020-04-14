using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Notifications.Models;
using Application.Patients.Models;
using Domain.Enumerations;
using MediatR;
using Newtonsoft.Json;
using Patient = Domain.Entities.Patient;

namespace Application.Patients.Commands
{
    public sealed class SavePatient
    {
        public sealed class Command : IRequest<Guid>
        {
            [JsonConstructor]
            public Command(string name, DateTime dateOfBirth, int gender, Address address, string mobileNumber)
            {
                Name = name;
                DateOfBirth = dateOfBirth;
                Gender = gender;
                Address = address;
                MobileNumber = mobileNumber;
            }

            public string Name { get; }
            public DateTime DateOfBirth { get; }
            public int Gender { get; }
            public Address Address { get; }
            public string MobileNumber { get; }
        }

        public sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IAppDbRepository _appDbRepository;
            private readonly INotificationService _notificationService;

            public Handler(IAppDbRepository appDbRepository, INotificationService notificationService)
            {
                _appDbRepository = appDbRepository ??
                                   throw new ArgumentNullException(nameof(appDbRepository));
                _notificationService =
                    notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingPatient = await _appDbRepository.GetPatientIdByMobileNumberAsync(request.MobileNumber);
                if (existingPatient != null) throw new SystemException();

                var patientHospitalId = await _appDbRepository.GetHospitalIdByZipCode(request.Address.ZipCode);
                var patientId = Guid.NewGuid();
                var patient = new Patient
                {
                    PatientId = patientId,
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
                await _appDbRepository.AddPatientAsync(patient);
                _notificationService.Send(new Message
                {
                    Text = $"Patient created with the id {patientId}",
                    Payload = patient
                });
                return patientId;
            }
        }
    }
}