using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Application.Notifications.Models;
using GovHospitalApp.Core.Application.Patients.Models;
using GovHospitalApp.Core.Domain.Enumerations;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Infrastructure.Patients.Commands
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
                _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {

                var existingPatient = await _appDbRepository.GetPatientIdByMobileNumberAsync(request.MobileNumber);
                if (existingPatient != null)
                {
                    throw new SystemException();
                }

                var patientHospitalId = await _appDbRepository.GetHospitalIdByZipCode(request.Address.ZipCode);
                var patientId = Guid.NewGuid();
                var patient = new Domain.Entities.Patient()
                {
                    PatientId = patientId,
                    Name = request.Name,
                    DateOfBirth = request.DateOfBirth,
                    Gender = (GenderType)request.Gender,
                    MobileNumber = request.MobileNumber,
                    HospitalId = patientHospitalId,
                    Address = new Domain.Entities.Address()
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        State = request.Address.State,
                        ZipCode = request.Address.ZipCode
                    }
                };
                await _appDbRepository.AddPatientAsync(patient);
                _notificationService.Send(new Message()
                {
                    text = $"Patient created with the id {patientId}",
                    payload = patient
                });
                return patientId;
            }
        }
    }
}
