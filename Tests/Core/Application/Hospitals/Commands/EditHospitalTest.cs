using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Hospitals.Commands;
using Application.Hospitals.Models;
using Application.Interfaces;
using Moq;
using Xunit;
using Hospital = Domain.Entities.Hospital;

namespace Tests.Core.Application.Hospitals.Commands
{
    public class EditHospitalTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new EditHospital.Handler(null));
        }


        [Fact]
        public async Task Handle_Given_ValidRequest_Should_EditHospital()
        {
            // Arrange
            var hospitalId = new Guid();
            var command = new EditHospital.Command(hospitalId, "Lokesh Chandawar",
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
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

            var handler = new EditHospital.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.Equals(hospitalId));
        }
    }
}