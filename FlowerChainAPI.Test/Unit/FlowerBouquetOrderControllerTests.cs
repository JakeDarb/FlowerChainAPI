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
    public class FlowerBouquetOrderControllerTests : IDisposable
    {
        private readonly Mock<ILogger<FlowerBouquetOrderController>> _loggerMock;
        private readonly Mock<IFlowerBouquetOrderRepository> _flowerbouquetorderRepoMock;
        private readonly FlowerBouquetOrderController _flowerbouquetorderController;

        public FlowerBouquetOrderControllerTests()
        {
             _loggerMock = new Mock<ILogger<FlowerBouquetOrderController>>(MockBehavior.Loose);
            _flowerbouquetorderRepoMock = new Mock<IFlowerBouquetOrderRepository>(MockBehavior.Strict);
            _flowerbouquetorderController = new FlowerBouquetOrderController(_flowerbouquetorderRepoMock.Object, _loggerMock.Object);
        }
          public void Dispose()
        {
            _loggerMock.VerifyAll();
            _flowerbouquetorderRepoMock.VerifyAll();

            _loggerMock.Reset();
            _flowerbouquetorderRepoMock.Reset();
        }


        [Fact]
        public async Task TestGetAllFlowerBouquetOrders()
        {
            var returnSet = new[]
            {
               new FlowerBouquetOrder
                {
                    id = 1,
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
                    
                },
                new FlowerBouquetOrder
                {
                   id = 2,
                    orderId = 2,
                    flowerBouquetId = 2,
                    amount = 1
                    
                },
                new FlowerBouquetOrder
                {
                    id = 3,
                    orderId = 3,
                    flowerBouquetId = 3,
                    amount = 1  
                    
                }
            };
            // Arrange
            _flowerbouquetorderRepoMock.Setup(x => x.GetAllFlowerBouquetOrders()).Returns(Task.FromResult((IEnumerable<FlowerBouquetOrder>)returnSet)).Verifiable();

            // Act
            var flowerbouquetorderResponse = await _flowerbouquetorderController.GetAllOrders();

            // Assert
            flowerbouquetorderResponse.Should().BeOfType<OkObjectResult>();

            Snapshot.Match(flowerbouquetorderResponse);
            }


            [Fact]
            public async Task TestGetOnFlowerBouquetOrderHappyPath(){

                var order = new FlowerBouquetOrder()
                {
                    id = 1,
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
                };
                _flowerbouquetorderRepoMock.Setup(x => x.GetOneFlowerBouquetOrderById(1)).Returns(Task.FromResult(order)).Verifiable();
                var flowerbouquetorderResponse = await _flowerbouquetorderController.FlowerBouquetOrderById(1);
                flowerbouquetorderResponse.Should().BeOfType<OkObjectResult>();
                Snapshot.Match(flowerbouquetorderResponse);
            }


            [Fact]
            public async Task TestGetOneFlowerBouquetOrderNotFound(){

        
                _flowerbouquetorderRepoMock.Setup(x => x.GetOneFlowerBouquetOrderById(1)).Returns(Task.FromResult(null as FlowerBouquetOrder)).Verifiable();
                var flowerbouquetorderResponse = await _flowerbouquetorderController.FlowerBouquetOrderById(1);
                flowerbouquetorderResponse.Should().BeOfType<NotFoundResult>();
                Snapshot.Match(flowerbouquetorderResponse);
            }


            [Fact]
           public async Task TestInsertOneOrder(){
               var order = new FlowerBouquetOrder
               {
                    id = 1,
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
               };
               _flowerbouquetorderRepoMock.Setup(x => x.Insert(1,1,1)).Returns(Task.FromResult(order)).Verifiable();
               var flowerbouquetorderResponse = await _flowerbouquetorderController.CreateFlowerBouquetOrder(new FlowerBouquetOrderUpsertInput()
               {
                    
                    
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1

               });
               flowerbouquetorderResponse.Should().BeOfType<CreatedResult>();
               Snapshot.Match(flowerbouquetorderResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerBouquetOrderHappyPath()
           {
               var order = new FlowerBouquetOrder()
               {
                    id = 1,
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
               };
                _flowerbouquetorderRepoMock.Setup(x => x.Update(1,1,1,1)).Returns(Task.FromResult(order)).Verifiable();
                var flowerbouquetorderResponse = await _flowerbouquetorderController.UpdateFlowerBouquetOrder(1,new FlowerBouquetOrderUpsertInput()
               {
                   
                    
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1

               });
               flowerbouquetorderResponse.Should().BeOfType<AcceptedResult>();
               Snapshot.Match(flowerbouquetorderResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerBouquetOrderNotFound()
           {
               _flowerbouquetorderRepoMock.Setup(x => x.Update(1,1,1,1)).Throws<NotFoundException>().Verifiable();
                var flowerbouquetorderResponse = await _flowerbouquetorderController.UpdateFlowerBouquetOrder(1,new FlowerBouquetOrderUpsertInput()
               {
                   
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1

               });
               flowerbouquetorderResponse.Should().BeOfType<NotFoundResult>();
               Snapshot.Match(flowerbouquetorderResponse);
           }

    }
}
