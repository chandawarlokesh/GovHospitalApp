using GovHospitalApp.Core.Application.Infrastructure.Patients.Queries;
using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core.Application.Patients.Queries
{
    public class GetPatientByHospitalIdTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetPatientsByHospitalId.Handler(null));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_Empty()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var query = new GetPatientsByHospitalId.Query(hospitalId);

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientsByHospitalIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((IEnumerable<Patient>)null);

            var handler = new GetPatientsByHospitalId.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_List()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var query = new GetPatientsByHospitalId.Query(hospitalId);
            var patients = new List<Patient>
            {
                new Patient()
                {
                    Address = new Address(),
                    HospitalId = hospitalId
                },
                new Patient()
                {
                    Address = new Address(),
                    HospitalId = hospitalId
                },
                new Patient()
                {
                    Address = new Address(),
                    HospitalId = hospitalId
                }
            };
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientsByHospitalIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(patients);

            var handler = new GetPatientsByHospitalId.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(3, result.Count());
            Assert.All(result, item => Assert.Equal(hospitalId, item.HospitalId.Value));
        }
    }
}
