using GovHospitalApp.Core.Application.Exceptions;
using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Queries;
using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core.Application.Hospitals.Queries
{
    public class GetHospitalTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetHospital.Handler(null));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetHospitals_Return_Empty()
        {
            // Arrange
            var query = new GetHospital.Query(Guid.NewGuid());

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetHospitalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Hospital)null);

            var handler = new GetHospital.Handler(mockAppDbRepository.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetHospital_Return_Hospital()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var query = new GetHospital.Query(hospitalId);
            var hospital = new Hospital()
            {
                HospitalId = hospitalId,
                Address = new Address()
            };

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetHospitalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(hospital);

            var handler = new GetHospital.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(hospitalId, result.Id);
        }
    }
}
