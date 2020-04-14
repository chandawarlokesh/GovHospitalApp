using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Notifications.Models;
using Application.Patients.Commands;
using Domain.Entities;
using Moq;
using Xunit;
using Address = Application.Patients.Models.Address;
using Patient = Domain.Entities.Patient;

namespace Tests.Core.Application.Patients.Commands
{
    public class EditPatientTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            var mockNotificationService = new Mock<INotificationService>();
            Assert.Throws<ArgumentNullException>(() => new EditPatient.Handler(null, mockNotificationService.Object));
        }

        [Fact]
        public void Handle_Given_InValid_NotificationService_DI_Should_Throw_Exception()
        {
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            Assert.Throws<ArgumentNullException>(() => new EditPatient.Handler(mockAppDbRepository.Object, null));
        }

        [Fact]
        public async Task Handle_Given_InValidRequest_HospitalId_Should_Not_EditPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var hospitalId = Guid.NewGuid();
            var command = new EditPatient.Command(patientId, "Lokesh Chandawar",
                DateTime.Now, 0,
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312", hospitalId);

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            mockNotificationService
                .Setup(x => x.Send(It.IsAny<Message>()));


            mockAppDbRepository
                .Setup(x => x.GetPatientByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Patient
                {
                    PatientId = patientId
                });

            mockAppDbRepository
                .Setup(x => x.GetHospitalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Hospital) null);

            mockAppDbRepository
                .Setup(x => x.EditHospitalByIdAsync(It.IsAny<Guid>(), It.IsAny<Hospital>()))
                .Returns(Task.CompletedTask);

            var handler = new EditPatient.Handler(mockAppDbRepository.Object, mockNotificationService.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_Given_InValidRequest_PatientId_Should_Not_EditPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var hospitalId = Guid.NewGuid();
            var command = new EditPatient.Command(patientId, "Lokesh Chandawar",
                DateTime.Now, 0,
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312", hospitalId);

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            mockNotificationService
                .Setup(x => x.Send(It.IsAny<Message>()));


            mockAppDbRepository
                .Setup(x => x.GetPatientByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Patient) null);

            mockAppDbRepository
                .Setup(x => x.GetHospitalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Hospital) null);

            mockAppDbRepository
                .Setup(x => x.EditHospitalByIdAsync(It.IsAny<Guid>(), It.IsAny<Hospital>()))
                .Returns(Task.CompletedTask);

            var handler = new EditPatient.Handler(mockAppDbRepository.Object, mockNotificationService.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, new CancellationToken()));
        }

        [Fact]
        public async Task Handle_Given_ValidRequest_Should_EditPatient()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var hospitalId = Guid.NewGuid();
            var command = new EditPatient.Command(patientId, "Lokesh Chandawar",
                DateTime.Now, 0,
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312", hospitalId);

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            var mockNotificationService = new Mock<INotificationService>();

            mockNotificationService
                .Setup(x => x.Send(It.IsAny<Message>()));


            mockAppDbRepository
                .Setup(x => x.GetPatientByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Patient
                    {
                        PatientId = patientId
                    });

            mockAppDbRepository
                .Setup(x => x.GetHospitalByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new Hospital
                    {
                        HospitalId = hospitalId
                    });

            mockAppDbRepository
                .Setup(x => x.EditHospitalByIdAsync(It.IsAny<Guid>(), It.IsAny<Hospital>()))
                .Returns(Task.CompletedTask);

            var handler = new EditPatient.Handler(mockAppDbRepository.Object, mockNotificationService.Object);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Equals(patientId));
        }
    }
}