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
    public class GetPatientsTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetPatients.Handler(null));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_Empty()
        {
            // Arrange
            var query = new GetPatients.Query();

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientsAsync())
                .ReturnsAsync((IEnumerable<Patient>)null);

            var handler = new GetPatients.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetPatients_Return_List()
        {
            // Arrange
            var query = new GetPatients.Query();
            var patients = new List<Patient>
            {
                new Patient()
                {
                    Address = new Address()
                },
                new Patient()
                {
                    Address = new Address()
                },
                new Patient()
                {
                    Address = new Address()
                }
            };
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetPatientsAsync())
                .ReturnsAsync(patients);

            var handler = new GetPatients.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}
