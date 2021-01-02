using System;
using System.Collections.Generic;
using Xunit;
using FlowerChainAPI.Controller;
using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snapshooter.Xunit;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace FlowerChainAPI.Test.Unit
{
    public class FlowerBouquetControllerTests : IDisposable
    {
        private readonly Mock<ILogger<FlowerBouquetController>> _loggerMock;
        private readonly Mock<IFlowerBouquetRepository> _flowerBouquetRepoMock;
        private readonly FlowerBouquetController _flowerBouquetController;

        public FlowerBouquetControllerTests()
        {
             _loggerMock = new Mock<ILogger<FlowerBouquetController>>(MockBehavior.Loose);
            _flowerBouquetRepoMock = new Mock<IFlowerBouquetRepository>(MockBehavior.Strict);
            _flowerBouquetController = new FlowerBouquetController(_flowerBouquetRepoMock.Object, _loggerMock.Object);
        }
          public void Dispose()
        {
            _loggerMock.VerifyAll();
            _flowerBouquetRepoMock.VerifyAll();

            _loggerMock.Reset();
            _flowerBouquetRepoMock.Reset();
        }

        [Fact]
        
        
        public async Task TestGetAllflowerBouquets()
        {
            var returnSet = new[]
            {
               new FlowerBouquet
                {
                    id = 1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1"
                },
                new FlowerBouquet
                {
                   id = 1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1" 
                },
                new FlowerBouquet
                {
                    id = 1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1"
                },
            };
            // Arrange
            _flowerBouquetRepoMock.Setup(x => x.GetAllBouquets()).Returns(Task.FromResult((IEnumerable<FlowerBouquet>)returnSet)).Verifiable();

            // Act
            var bouquetsResponse = await _flowerBouquetController.GetAllBouquets();

            // Assert
            bouquetsResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(bouquetsResponse);
            }
        }



}
