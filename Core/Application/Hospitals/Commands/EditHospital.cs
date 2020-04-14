using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Hospitals.Models;
using Application.Interfaces;
using MediatR;
using Newtonsoft.Json;

namespace Application.Hospitals.Commands
{
    public sealed class EditHospital
    {
        public sealed class Command : IRequest<Guid>
        {
            [JsonConstructor]
            public Command(Guid id, string name, Address address, string mobileNumber)
            {
                Id = id;
                Name = name;
                Address = address;
                MobileNumber = mobileNumber;
            }

            public Guid Id { get; }
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
                var existingHospital = await _appDbRepository.GetHospitalByIdAsync(request.Id);
                if (existingHospital == null) throw new NotFoundException(nameof(Hospital), request.Id);

                var hospital = new Domain.Entities.Hospital
                {
                    HospitalId = request.Id,
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
                await _appDbRepository.EditHospitalByIdAsync(request.Id, hospital);
                return request.Id;
            }
        }
    }
}