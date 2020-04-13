using GovHospitalApp.Core.Application.Infrastructure.Patients.Commands;
using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Application.Notifications.Models;
using GovHospitalApp.Core.Application.Patients.Models;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GovHospitalApp.Tests.Core.Applications.Hospitals.Commands
{
    public class SavePatientTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            var mockNotificationService = new Mock<INotificationService>();
            Assert.Throws<ArgumentNullException>(() => new SavePatient.Handler(null, mockNotificationService.Object));
        }

        [Fact]
        public void Handle_Given_InValid_NotificationService_DI_Should_Throw_Exception()
        {
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            Assert.Throws<ArgumentNullException>(() => new SavePatient.Handler(mockAppDbRepository.Object, null));
        }

        [Fact]
        public async Task Handle_Given_ValidRequest_Should_SavePatient()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var command = new SavePatient.Command("Lokesh Chandawar",
                DateTime.Now, 0,
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            mockNotificationService
                .Setup(x => x.Send(It.IsAny<Message>()));

            mockAppDbRepository
                .Setup(x => x.GetPatientIdByMobileNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(
                (GovHospitalApp.Core.Domain.Entities.Patient)null);

            mockAppDbRepository
                .Setup(x => x.GetHospitalIdByZipCode(It.IsAny<string>()))
                .ReturnsAsync((Guid?)hospitalId);

            mockAppDbRepository
                .Setup(x => x.AddPatientAsync(It.IsAny<GovHospitalApp.Core.Domain.Entities.Patient>()))
                .Returns(Task.CompletedTask);

            var handler = new SavePatient.Handler(mockAppDbRepository.Object, mockNotificationService.Object);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Handle_Given_InValidRequest_ExistingMobileNumber_Should_Not_SavePatient()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var command = new SavePatient.Command("Lokesh Chandawar",
                DateTime.Now, 0,
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            mockNotificationService
                .Setup(x => x.Send(It.IsAny<Message>()));

            mockAppDbRepository
                .Setup(x => x.GetPatientIdByMobileNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(
                new GovHospitalApp.Core.Domain.Entities.Patient()
                {
                    PatientId = Guid.NewGuid()
                });

            var handler = new SavePatient.Handler(mockAppDbRepository.Object, mockNotificationService.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<SystemException>(() => handler.Handle(command, new CancellationToken()));
        }
    }
}
