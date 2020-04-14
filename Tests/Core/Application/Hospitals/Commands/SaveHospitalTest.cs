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
    public class SaveHospitalTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new SaveHospital.Handler(null));
        }


        [Fact]
        public async Task Handle_Given_ValidRequest_Should_SaveHospital_Return_HospitalId()
        {
            // Arrange
            var command = new SaveHospital.Command("Lokesh Chandawar",
                new Address("Vanaz cornor, Kothrud", "Pune", "Maharashtra", "410038"), "9021433312");

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.AddHospitalAsync(It.IsAny<Hospital>()))
                .Returns(Task.CompletedTask);

            var handler = new SaveHospital.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}