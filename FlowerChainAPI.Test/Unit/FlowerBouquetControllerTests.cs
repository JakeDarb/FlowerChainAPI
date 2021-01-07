using System;
using System.Collections.Generic;
using Xunit;
using FlowerChainAPI.Controller;
using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Models.Web;
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
    // Testing if the controller works
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
                   id = 2,
                    bouquetName = "test name 2",
                    price = 456,
                    amountSold = 2,
                    description = "test description 2" 
                },
                new FlowerBouquet
                {
                    id = 3,
                    bouquetName = "test name 3",
                    price = 789,
                    amountSold = 3,
                    description = "test description 3"
                }
            };
            // Arrange
            _flowerBouquetRepoMock.Setup(x => x.GetAllBouquets()).Returns(Task.FromResult((IEnumerable<FlowerBouquet>)returnSet)).Verifiable();

            // Act
            var bouquetsResponse = await _flowerBouquetController.GetAllBouquets();

            // Assert
            bouquetsResponse.Should().BeOfType<OkObjectResult>();

            Snapshot.Match(bouquetsResponse);
            }


            [Fact]
            public async Task TestGetOnFlowerBouquetHappyPath(){

                var bouquet = new FlowerBouquet()
                {
                    id = 1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1"
                };
                _flowerBouquetRepoMock.Setup(x => x.GetOneBouquetById(1)).Returns(Task.FromResult(bouquet)).Verifiable();
                var bouquetsResponse = await _flowerBouquetController.FlowerBouquetById(1);
                bouquetsResponse.Should().BeOfType<OkObjectResult>();
                Snapshot.Match(bouquetsResponse);
            }


            [Fact]
            public async Task TestGetOnFlowerBouquetNotFound(){

        
                _flowerBouquetRepoMock.Setup(x => x.GetOneBouquetById(1)).Returns(Task.FromResult(null as FlowerBouquet)).Verifiable();
                var bouquetsResponse = await _flowerBouquetController.FlowerBouquetById(1);
                bouquetsResponse.Should().BeOfType<NotFoundResult>();
                Snapshot.Match(bouquetsResponse);
            }


            [Fact]
           public async Task TestInsertOneFlowerBouquet(){
               var bouquet = new FlowerBouquet
               {
                    id=1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1"
               };
               _flowerBouquetRepoMock.Setup(x => x.Insert("test name", 456,2,"test description 2")).Returns(Task.FromResult(bouquet)).Verifiable();
               var bouquetResponse = await _flowerBouquetController.CreateFlowerBouquet(new FlowerBouquetUpsertInput()
               {
                    
                    bouquetName = "test name",
                    price = 456,
                    amountSold = 2,
                    description = "test description 2"

               });
               bouquetResponse.Should().BeOfType<CreatedResult>();
               Snapshot.Match(bouquetResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerBouquetHappyPath()
           {
               var bouquet = new FlowerBouquet()
               {
                    id = 1,
                    bouquetName = "test name 1",
                    price = 123,
                    amountSold = 1,
                    description = "test description 1"
               };
                _flowerBouquetRepoMock.Setup(x => x.Update(1,"test name", 456,2,"test description 2")).Returns(Task.FromResult(bouquet)).Verifiable();
                var bouquetResponse = await _flowerBouquetController.UpdateFlowerBouquet(1,new FlowerBouquetUpsertInput()
               {
                   
                    bouquetName = "test name",
                    price = 456,
                    amountSold = 2,
                    description = "test description 2"

               });
               bouquetResponse.Should().BeOfType<AcceptedResult>();
               Snapshot.Match(bouquetResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerBouquetNotFound()
           {
               _flowerBouquetRepoMock.Setup(x => x.Update(1,"test name", 456,2,"test description 2")).Throws<NotFoundException>().Verifiable();
                var bouquetResponse = await _flowerBouquetController.UpdateFlowerBouquet(1,new FlowerBouquetUpsertInput()
               {
                   
                    bouquetName = "test name",
                    price = 456,
                    amountSold = 2,
                    description = "test description 2"

               });
               bouquetResponse.Should().BeOfType<NotFoundResult>();
               Snapshot.Match(bouquetResponse);
           }
           
    }
}
