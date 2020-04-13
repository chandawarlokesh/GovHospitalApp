using GovHospitalApp.Core.Application.Exceptions;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Models;
using GovHospitalApp.Core.Application.Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Queries
{
    public sealed class GetHospital
    {
        public sealed class Query : IRequest<Hospital>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; }
        }

        public sealed class Handler : IRequestHandler<Query, Hospital>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                            throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<Hospital> Handle(Query request, CancellationToken cancellationToken)
            {
                var hospital = await _appDbRepository.GetHospitalByIdAsync(request.Id);
                if (hospital == null)
                {
                    throw new NotFoundException(nameof(Hospital), request.Id);
                }
                return ToHospitalViewModel(hospital);
            }

            private static Hospital ToHospitalViewModel(Domain.Entities.Hospital hospital)
            {
                var hospitalAddress = new Address(hospital.Address.Street, hospital.Address.City,
                    hospital.Address.State, hospital.Address.ZipCode);

                return new Hospital(hospital.HospitalId, hospital.Name, hospital.MobileNumber, hospitalAddress);
            }
        }
    }
}
