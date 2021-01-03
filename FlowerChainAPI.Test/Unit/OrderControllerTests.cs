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
    public class OrderControllerTests : IDisposable
    {
        private readonly Mock<ILogger<OrderController>> _loggerMock;
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly OrderController _orderController;

        public OrderControllerTests()
        {
             _loggerMock = new Mock<ILogger<OrderController>>(MockBehavior.Loose);
            _orderRepoMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            _orderController = new OrderController(_orderRepoMock.Object, _loggerMock.Object);
        }
          public void Dispose()
        {
            _loggerMock.VerifyAll();
            _orderRepoMock.VerifyAll();

            _loggerMock.Reset();
            _orderRepoMock.Reset();
        }

        [Fact]
        
        
        public async Task TestGetAllOrders()
        {
            var returnSet = new[]
            {
               new Order
                {
                    id = 1,
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"
                    
                },
                new Order
                {
                   id = 2,
                    dateTimeOrder = "testdatetime 2",
                    personId = "testpersonid 2"
                    
                },
                new Order
                {
                    id = 3,
                    dateTimeOrder = "testdatetime 3",
                    personId = "testpersonid 3"
                    
                }
            };
            // Arrange
            _orderRepoMock.Setup(x => x.GetAllOrders()).Returns(Task.FromResult((IEnumerable<Order>)returnSet)).Verifiable();

            // Act
            var orderResponse = await _orderController.GetAllOrders();

            // Assert
            orderResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(orderResponse);
            }

            [Fact]
            public async Task TestGetOnOrderHappyPath(){

                var order = new Order()
                {
                    id = 1,
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"
                };
                _orderRepoMock.Setup(x => x.GetOneOrderById(1)).Returns(Task.FromResult(order)).Verifiable();
                var orderResponse = await _orderController.OrderById(1);
                orderResponse.Should().BeOfType<OkObjectResult>();
                Snapshot.Match(orderResponse);
            }

            [Fact]
            public async Task TestGetOnOrderNotFound(){

        
                _orderRepoMock.Setup(x => x.GetOneOrderById(1)).Returns(Task.FromResult(null as Order)).Verifiable();
                var orderResponse = await _orderController.OrderById(1);
                orderResponse.Should().BeOfType<NotFoundResult>();
                Snapshot.Match(orderResponse);
            }


            [Fact]
           public async Task TestInsertOneOrder(){
               var order = new Order
               {
                    id = 1,
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"
               };
               _orderRepoMock.Setup(x => x.Insert(1,"testdatetime 1","testpersonid 1")).Returns(Task.FromResult(order)).Verifiable();
               var orderResponse = await _orderController.CreateOrder(new OrderPostUpsertInput()
               {
                    id = 1,
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"

               });
               orderResponse.Should().BeOfType<CreatedResult>();
               Snapshot.Match(orderResponse);
           }

           [Fact]
           public async Task TestUpdateOneOrderHappyPath()
           {
               var order = new Order()
               {
                    id = 1,
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"
               };
                _orderRepoMock.Setup(x => x.Update(1,"testdatetime 1","testpersonid 1")).Returns(Task.FromResult(order)).Verifiable();
                var orderResponse = await _orderController.UpdateOrder(1,new OrderPatchUpsertInput()
               {
                   
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"

               });
               orderResponse.Should().BeOfType<AcceptedResult>();
               Snapshot.Match(orderResponse);
           }

           [Fact]
           public async Task TestUpdateOneOrderNotFound()
           {
               _orderRepoMock.Setup(x => x.Update(1,"testdatetime 1","testpersonid 1")).Throws<NotFoundException>().Verifiable();
                var orderResponse = await _orderController.UpdateOrder(1,new OrderPatchUpsertInput()
               {
                   
                    dateTimeOrder = "testdatetime 1",
                    personId = "testpersonid 1"

               });
               orderResponse.Should().BeOfType<NotFoundResult>();
               Snapshot.Match(orderResponse);
           }

           





        }



}
