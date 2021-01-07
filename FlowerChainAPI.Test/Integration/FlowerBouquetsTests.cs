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
    // Testing if database works
    public class FlowerBouquetsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FlowerBouquetsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetFlowerBouquetsEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowerbouquet");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerBouquetsEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquet.Add(new FlowerBouquet() {id = 1, bouquetName = "test name 1", price = 123, amountSold = 1, description = "test description 1"});
                db.FlowerBouquet.Add(new FlowerBouquet() {id = 2, bouquetName = "test name 2", price = 456, amountSold = 2, description = "test description 2"});
            });
            var response = await client.GetAsync("flowerchainapi/flowerbouquet");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerBouquetById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowerbouquet/1");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task GetFlowerBouquetByIdReturnFlowerBouquetIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquet.Add(new FlowerBouquet() {id = 1, bouquetName = "test name 1", price = 123, amountSold = 1, description = "test description 1"});
            });
            var response = await client.GetAsync("flowerchainapi/flowerbouquet/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task DeleteFlowerBouquetByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquet.Add(new FlowerBouquet() {id = 1, bouquetName = "test name 1", price = 123, amountSold = 1, description = "test description 1"});

            });
            var response = await client.DeleteAsync("flowerchainapi/flowerbouquet/2");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task DeleteFlowerBouquetByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquet.Add(new FlowerBouquet() {id = 1, bouquetName = "test name 1", price = 123, amountSold = 1, description = "test description 1"});

            });
            var beforeDeleteResponse = await client.GetAsync("flowerchainapi/flowerbouquet/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("flowerchainapi/flowerbouquet/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("flowerchainapi/flowerbouquet/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task InsertFlowerBouquetReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetUpsertInput
                {
                    
                    bouquetName = "testbouquetname 1",
                    price = 123,
                    amountSold = 1,
                    description = "testdescription 1"
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowerbouquet", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerBouquetWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.bouquetName.Should().Be("testbouquetname 1");
            body.price.Should().Be(123);
            body.amountSold.Should().Be(1);
            body.description.Should().Be("testdescription 1");
            var getResponse = await client.GetAsync($"flowerchainapi/flowerbouquet/{body.id}");
            getResponse.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task InsertFlowerBouquetThrowsErrorOnEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetUpsertInput
                {
                    
                    bouquetName = string.Empty,
                    price = 0,
                    amountSold = 0,
                    description = string.Empty
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowerbouquet", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }


        [Fact]
        public async Task UpdateFlowerBouquetReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetUpsertInput
                {
                    bouquetName = "testbouquetname 1",
                    price = 123,
                    amountSold = 1,
                    description = "testdescription 1"
                }
            };
            var patchResponse = await client.PatchAsync("flowerchainapi/flowerbouquet/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }
        
    }
}