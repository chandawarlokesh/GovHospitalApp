using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Models;
using GovHospitalApp.Core.Application.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Queries
{
    public sealed class GetHospitals
    {
        public sealed class Query : IRequest<IEnumerable<Hospital>>
        {
        }

        public sealed class Handler : IRequestHandler<Query, IEnumerable<Hospital>>
        {
            private readonly IAppDbRepository _appDbRepository;

            public Handler(IAppDbRepository appDbRepository)
            {
                _appDbRepository = appDbRepository ??
                                            throw new ArgumentNullException(nameof(appDbRepository));
            }

            public async Task<IEnumerable<Hospital>> Handle(Query request, CancellationToken cancellationToken)
            {
                var hospitals = await _appDbRepository.GetHospitalsAsync();

                return hospitals?.Select(ToHospitalViewModel);
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
