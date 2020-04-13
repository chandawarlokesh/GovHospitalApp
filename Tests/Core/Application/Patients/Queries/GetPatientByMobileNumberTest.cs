using GovHospitalApp.Core.Application.Infrastructure.Patients.Queries;
using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core.Application.Patients.Queries
{
    public class GetPatientByMobileNumberTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetPatientByMobileNumber.Handler(null));
        }

        [Fact]
        public async Task Handle_Fetch_Given_Empty_Should_GetPatient_Should_Throw_Exception()
        {
            // Arrange
            var query = new GetPatientByMobileNumber.Query("");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientIdByMobileNumberAsync(It.IsAny<string>()))
                .ReturnsAsync((Patient)null);

            var handler = new GetPatientByMobileNumber.Handler(mockAppDbRepository.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_Fetch_Given_Invalid_MobileNumber_Should_GetPatient_Should_Throw_Exception()
        {
            // Arrange
            var query = new GetPatientByMobileNumber.Query("9021433312");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientIdByMobileNumberAsync(It.IsAny<string>()))
                .ReturnsAsync((Patient)null);

            var handler = new GetPatientByMobileNumber.Handler(mockAppDbRepository.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_List()
        {
            // Arrange
            var mobileNumber = "9021433312";
            var query = new GetPatientByMobileNumber.Query(mobileNumber);
            var patient = new Patient()
            {
                PatientId = Guid.NewGuid(),
                Address = new Address(),
                MobileNumber = mobileNumber
            };
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientIdByMobileNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(patient);

            var handler = new GetPatientByMobileNumber.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
