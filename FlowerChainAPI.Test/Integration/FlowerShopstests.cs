using System.Threading.Tasks;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Models.Web;
using FlowerChainAPI.Tests.Integration.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Snapshooter;
using Snapshooter.Xunit;
using Xunit;

namespace FlowerChainAPI.Tests.Integration
{
    
    public class FlowerShopsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FlowerShopsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetFlowerShopsEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowershop");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerShopsEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerShop.Add(new FlowerShop() {id = 1, shopName = "testshopname 1", streetName="test streetname 1", houseNumber="test housenumber 1", city="testcity 1", postalCode="testpostalcode 1", phoneNumber="testpostalcode 1", email="testemail 1"});
                db.FlowerShop.Add(new FlowerShop() {id = 2, shopName = "testshopname 2", streetName="test streetname 2", houseNumber="test housenumber 2", city="testcity 2", postalCode="testpostalcode 2", phoneNumber="testpostalcode 2", email="testemail 2"});
            });
            var response = await client.GetAsync("flowerchainapi/flowershop");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerShopById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowershop/1");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task GetFlowerShopByIdReturnFlowerBouquetIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerShop.Add(new FlowerShop() {id = 1, shopName = "testshopname 1", streetName="test streetname 1", houseNumber="test housenumber 1", city="testcity 1", postalCode="testpostalcode 1", phoneNumber="testpostalcode 1", email="testemail 1"});
            });
            var response = await client.GetAsync("flowerchainapi/flowershop/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task DeleteFlowerShopByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerShop.Add(new FlowerShop() {id = 1, shopName = "testshopname 1", streetName="test streetname 1", houseNumber="test housenumber 1", city="testcity 1", postalCode="testpostalcode 1", phoneNumber="testpostalcode 1", email="testemail 1"});

            });
            var response = await client.DeleteAsync("flowerchainapi/flowershop/2");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task DeleteFlowerShopByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerShop.Add(new FlowerShop() {id = 1, shopName = "testshopname 1", streetName="test streetname 1", houseNumber="test housenumber 1", city="testcity 1", postalCode="testpostalcode 1", phoneNumber="testphonenumbercode 1", email="testemail 1"});

            });
            var beforeDeleteResponse = await client.GetAsync("flowerchainapi/flowershop/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("flowerchainapi/flowershop/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("flowerchainapi/flowershop/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task InsertFlowerShopReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerShopUpsertInput
                {
                    
                    shopName = "testshopname 1", 
                    streetName="teststreetname 1", 
                    houseNumber="testhousenumber 1", 
                    city="testcity 1", 
                    postalCode="testpostalcode 1", 
                    phoneNumber="testphonenumber 1", 
                    email="testemail 1"

                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowershop", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerShopWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.shopName.Should().Be("testshopname 1");
            body.streetName.Should().Be("teststreetname 1");
            body.houseNumber.Should().Be("testhousenumber 1");
            body.city.Should().Be("testcity 1");
            body.postalCode.Should().Be("testpostalcode 1");
            body.phoneNumber.Should().Be("testphonenumber 1");
            body.email.Should().Be("testemail 1");
            var getResponse = await client.GetAsync($"flowerchainapi/flowershop/{body.id}");
            getResponse.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task InsertFlowerShopThrowsErrorOnEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerShopUpsertInput
                {
                   
                    shopName = string.Empty,
                    streetName= string.Empty, 
                    houseNumber= string.Empty, 
                    city= string.Empty,
                    postalCode= string.Empty,
                    phoneNumber= string.Empty, 
                    email= string.Empty
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowershop", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }


        [Fact]
        public async Task UpdateFlowerShopReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerShopUpsertInput
                {
                    shopName = "testshopname 1", 
                    streetName="test streetname 1", 
                    houseNumber="test housenumber 1", 
                    city="testcity 1", 
                    postalCode="testpostalcode 1", 
                    phoneNumber="testpostalcode 1", 
                    email="testemail 1"
                }
            };
            var patchResponse = await client.PatchAsync("flowerchainapi/flowershop/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

    }
}