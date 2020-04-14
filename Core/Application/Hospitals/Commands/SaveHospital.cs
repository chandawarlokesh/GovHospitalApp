using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Hospitals.Models;
using Application.Interfaces;
using MediatR;
using Newtonsoft.Json;
using Hospital = Domain.Entities.Hospital;

namespace Application.Hospitals.Commands
{
    public sealed class SaveHospital
    {
        public sealed class Command : IRequest<Guid>
        {
            [JsonConstructor]
            public Command(string name, Address address, string mobileNumber)
            {
                Name = name;
                Address = address;
                MobileNumber = mobileNumber;
            }

            public string Name { get; }
            public Address Address { get; }
            public string MobileNumber { get; }
        }

        public sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                   throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var hospitalId = Guid.NewGuid();
                var hospital = new Hospital
                {
                    HospitalId = hospitalId,
                    Name = request.Name,
                    MobileNumber = request.MobileNumber,
                    Address = new Domain.Entities.Address
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        State = request.Address.State,
                        ZipCode = request.Address.ZipCode
                    }
                };
                await _appDbRepository.AddHospitalAsync(hospital);
                return hospitalId;
            }
        }
    }
}