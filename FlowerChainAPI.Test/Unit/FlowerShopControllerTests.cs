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
    public class FlowerShopControllerTests : IDisposable
    {
        private readonly Mock<ILogger<FlowerShopController>> _loggerMock;
        private readonly Mock<IFlowerShopRepository> _flowerShopRepoMock;
        private readonly FlowerShopController _flowerShopController;

        public FlowerShopControllerTests()
        {
             _loggerMock = new Mock<ILogger<FlowerShopController>>(MockBehavior.Loose);
            _flowerShopRepoMock = new Mock<IFlowerShopRepository>(MockBehavior.Strict);
            _flowerShopController = new FlowerShopController(_flowerShopRepoMock.Object, _loggerMock.Object);
        }
          public void Dispose()
        {
            _loggerMock.VerifyAll();
            _flowerShopRepoMock.VerifyAll();

            _loggerMock.Reset();
            _flowerShopRepoMock.Reset();
        }


        [Fact]
        public async Task TestGetAllflowerShop()
        {
            var returnSet = new[]
            {
               new FlowerShop
                {
                    id = 1,
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1",
                },
                new FlowerShop
                {
                    id = 2,
                    shopName = "testshopname 2",
                    streetName = "teststreetname 2",
                    houseNumber = "testhousenumber 2",
                    city = "testcity 2",
                    postalCode = "testpostalcode 2",
                    phoneNumber = "testphonenumber 2",
                    email = "testemail 2",
                },
                new FlowerShop
                {
                    id = 3,
                    shopName = "testshopname 3",
                    streetName = "teststreetname 3",
                    houseNumber = "testhousenumber 3",
                    city = "testcity 3",
                    postalCode = "testpostalcode 3",
                    phoneNumber = "testphonenumber 3",
                    email = "testemail 3",
                },
               
            };
            // Arrange
            _flowerShopRepoMock.Setup(x => x.GetAllShops()).Returns(Task.FromResult((IEnumerable<FlowerShop>)returnSet)).Verifiable();

            // Act
            var shopsResponse = await _flowerShopController.GetAllFlowerShops();

            // Assert
            shopsResponse.Should().BeOfType<OkObjectResult>();

            Snapshot.Match(shopsResponse);
            }


            [Fact]
            public async Task TestGetOnFlowerShopHappyPath(){

                var shop = new FlowerShop()
                {
                    id = 1,
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"
                };
                _flowerShopRepoMock.Setup(x => x.GetOneShopById(1)).Returns(Task.FromResult(shop)).Verifiable();
                var shopsResponse = await _flowerShopController.FlowerShopById(1);
                shopsResponse.Should().BeOfType<OkObjectResult>();
                Snapshot.Match(shopsResponse);
            }


            [Fact]
            public async Task TestGetOnFlowerShopNotFound(){

        
                _flowerShopRepoMock.Setup(x => x.GetOneShopById(1)).Returns(Task.FromResult(null as FlowerShop)).Verifiable();
                var shopsResponse = await _flowerShopController.FlowerShopById(1);
                shopsResponse.Should().BeOfType<NotFoundResult>();
                Snapshot.Match(shopsResponse);
            }


            [Fact]
           public async Task TestInsertOneFlowerShop(){
               var shop = new FlowerShop
               {
                    id = 1,
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"
               };
               _flowerShopRepoMock.Setup(x => x.Insert("testshopname 1", "teststreetname 1", "testhousenumber 1", "testcity 1", "testpostalcode 1", "testphonenumber 1", "testemail 1")).Returns(Task.FromResult(shop)).Verifiable();
               var shopResponse = await _flowerShopController.CreateFlowerShop(new FlowerShopUpsertInput()
               {
                   
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"

               });
               shopResponse.Should().BeOfType<CreatedResult>();
               Snapshot.Match(shopResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerShopHappyPath()
           {
               var shop = new FlowerShop()
               {
                    id = 1,
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"
               };
                _flowerShopRepoMock.Setup(x => x.Update(1,"testshopname 1", "teststreetname 1", "testhousenumber 1", "testcity 1", "testpostalcode 1", "testphonenumber 1", "testemail 1")).Returns(Task.FromResult(shop)).Verifiable();
                var shopResponse = await _flowerShopController.UpdateFlowerShop(1,new FlowerShopUpsertInput()
               {
                   
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"

               });
               shopResponse.Should().BeOfType<AcceptedResult>();
               Snapshot.Match(shopResponse);
           }


           [Fact]
           public async Task TestUpdateOneFlowerShopNotFound()
           {
               _flowerShopRepoMock.Setup(x => x.Update(1,"testshopname 1", "teststreetname 1", "testhousenumber 1", "testcity 1", "testpostalcode 1", "testphonenumber 1", "testemail 1")).Throws<NotFoundException>().Verifiable();
                var shopResponse = await _flowerShopController.UpdateFlowerShop(1,new FlowerShopUpsertInput()
               {
                   
                    shopName = "testshopname 1",
                    streetName = "teststreetname 1",
                    houseNumber = "testhousenumber 1",
                    city = "testcity 1",
                    postalCode = "testpostalcode 1",
                    phoneNumber = "testphonenumber 1",
                    email = "testemail 1"

               });
               shopResponse.Should().BeOfType<NotFoundResult>();
               Snapshot.Match(shopResponse);
           }
           
    }
}