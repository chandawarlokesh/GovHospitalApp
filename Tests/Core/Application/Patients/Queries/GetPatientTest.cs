using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Application.Patients.Queries;
using Domain.Entities;
using Moq;
using Xunit;

namespace Tests.Core.Application.Patients.Queries
{
    public class GetPatientTest
    {
        [Fact]
        public async Task Handle_Fetch_Should_GetPatient_Return_Hospital()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var query = new GetPatient.Query(patientId);
            var patient = new Patient
            {
                PatientId = patientId,
                Address = new Address()
            };

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(patient);

            var handler = new GetPatient.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(patientId, result.Id);
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_Empty()
        {
            // Arrange
            var query = new GetPatient.Query(Guid.NewGuid());

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Patient) null);

            var handler = new GetPatient.Handler(mockAppDbRepository.Object);
            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, new CancellationToken()));
        }

        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetPatient.Handler(null));
        }
    }
}