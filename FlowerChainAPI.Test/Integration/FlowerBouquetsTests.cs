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
            var response = await client.GetAsync("/flowerbouquets");
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
            var response = await client.GetAsync("/flowerbouquets");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetFlowerBouquetById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("/flowerbouquet/1");
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
            var response = await client.GetAsync("/flowerbouquet/1");
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
            var response = await client.DeleteAsync("/flowerbouquets/2");
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
            var beforeDeleteResponse = await client.GetAsync("/flowerbouquets/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("/flowerbouquets/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("/flowerbouquets/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InsertFlowerBouquetReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetPostUpsertInput
                {
                    id = 1,
                    bouquetName = "testbouquetname 1",
                    price = 123,
                    amountSold = 1,
                    description = "testdescription 1"
                }
            };
            var createResponse = await client.PostAsync("/flowerbouquets", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerBouquetWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.id.Should().Be(1);
            body.bouquetName.Should().Be("testbouquetname 1");
            body.price.Should().Be(123);
            body.amountSold.Should().Be(1);
            body.description.Should().Be("testdescription 1");
            var getResponse = await client.GetAsync($"/flowerbouquets/{body.id}");
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertFlowerBouquetThrowsErrorOnEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetPostUpsertInput
                {
                    id = 1,
                    bouquetName = "testbouquetname 1",
                    price = 123,
                    amountSold = 1,
                    description = "testdescription 1"
                }
            };
            var createResponse = await client.PostAsync("/bouquets", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateFlowerBouquetReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetPatchUpsertInput
                {
                    bouquetName = "testbouquetname 1",
                    price = 123,
                    amountSold = 1,
                    description = "testdescription 1"
                }
            };
            var patchResponse = await client.PatchAsync("/flowerbouquets/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

}
}