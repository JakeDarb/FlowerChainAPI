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
    public class FlowerBouquetOrdersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FlowerBouquetOrdersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        public async Task GetFlowerBouquetOrdersEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowerbouquetorder");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerBouquetOrdersEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquetOrder.Add(new FlowerBouquetOrder() {id = 1, orderId = 1, flowerBouquetId = 1, amount = 1 });
                db.FlowerBouquetOrder.Add(new FlowerBouquetOrder() {id = 2, orderId = 1, flowerBouquetId = 1, amount = 1 });
                
            });
            var response = await client.GetAsync("flowerchainapi/flowerbouquetorder");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task GetFlowerBouquetOrderById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/flowerbouquetorder/1");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task GetFlowerBouquetOrderByIdReturnFlowerBouquetIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquetOrder.Add(new FlowerBouquetOrder() {id = 1, orderId = 1, flowerBouquetId = 1, amount = 1 });
            });
            var response = await client.GetAsync("flowerchainapi/flowerbouquetorder/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }


        [Fact]
        public async Task DeleteFlowerBouquetOrderByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquetOrder.Add(new FlowerBouquetOrder() {id = 1, orderId = 1, flowerBouquetId = 1, amount = 1 });
            });
            var response = await client.DeleteAsync("flowerchainapi/flowerbouquetorder/2");
            response.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task DeleteFlowerBouquetOrderByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.FlowerBouquetOrder.Add(new FlowerBouquetOrder() {id = 1, orderId = 1, flowerBouquetId = 1, amount = 1 });

            });
            var beforeDeleteResponse = await client.GetAsync("flowerchainapi/flowerbouquetorder/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("flowerchainapi/flowerbouquetorder/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("flowerchainapi/flowerbouquetorder/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }


        [Fact]
        public async Task InsertFlowerBouquetOrderReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetOrderUpsertInput
                {
                    
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowerbouquetorder", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerBouquetOrderWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.id.Should().Be(1);
            body.orderId.Should().Be(1);
            body.flowerBouquetId.Should().Be(1);
            body.amount.Should().Be(1);
            var getResponse = await client.GetAsync($"flowerchainapi/flowerbouquetorder/{body.id}");
            getResponse.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task InsertOrderThrowsErrorOnEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetOrderUpsertInput
                {
                    orderId = 0,
                    flowerBouquetId = 0,
                    amount = 0
                    
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/flowerbouquetorder", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }


        [Fact]
        public async Task UpdateOrderReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerBouquetOrderUpsertInput
                {
                    orderId = 1,
                    flowerBouquetId = 1,
                    amount = 1
                }
            };
            var patchResponse = await client.PatchAsync("flowerchainapi/flowerbouquetorder/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

    }
}