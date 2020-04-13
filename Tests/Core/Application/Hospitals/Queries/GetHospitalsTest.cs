using GovHospitalApp.Core.Application.Infrastructure.Hospitals.Queries;
using GovHospitalApp.Core.Application.Interface;
using GovHospitalApp.Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Core.Application.Hospitals.Queries
{
    public class GetHospitalsTest
    {
        [Fact]
        public void Handle_Given_InValid_AppDbRepository_DI_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new GetHospitals.Handler(null));
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetHospitals_Return_Empty()
        {
            // Arrange
            var query = new GetHospitals.Query();

            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetHospitalsAsync())
                .ReturnsAsync((IEnumerable<Hospital>)null);

            var handler = new GetHospitals.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_Fetch_Should_GetHospitals_Return_List()
        {
            // Arrange
            var query = new GetHospitals.Query();
            var hospitals = new List<Hospital>
            {
                new Hospital()
                {
                    Address = new Address()
                },
                new Hospital()
                {
                    Address = new Address()
                },
                new Hospital()
                {
                    Address = new Address()
                }
            };
            var mockAppDbRepository = new Mock<IAppDbRepository>();
            mockAppDbRepository
                .Setup(x => x.GetHospitalsAsync())
                .ReturnsAsync(hospitals);

            var handler = new GetHospitals.Handler(mockAppDbRepository.Object);
            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(3, result.Count());
        }
    }
}
