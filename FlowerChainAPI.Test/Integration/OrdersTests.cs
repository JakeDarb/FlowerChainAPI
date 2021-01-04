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
    public class OrdersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public OrdersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetOrdersEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/order");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetOrdersEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Order.Add(new Order() {id = 1, dateTimeOrder = "testdatetimeorder 1", personId = "testpersonid 1"});
                db.Order.Add(new Order() {id = 2, dateTimeOrder = "testdatetimeorder 2", personId = "testpersonid 2"});
            });
            var response = await client.GetAsync("flowerchainapi/order");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetOrderById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("flowerchainapi/order/1");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetOrderByIdReturnFlowerBouquetIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Order.Add(new Order() {id = 1, dateTimeOrder = "testdatetimeorder 1", personId = "testpersonid 1"});
            });
            var response = await client.GetAsync("flowerchainapi/order/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DeleteOrderByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Order.Add(new Order() {id = 1, dateTimeOrder = "testdatetimeorder 1", personId = "testpersonid 1"});
            });
            var response = await client.DeleteAsync("flowerchainapi/order/2");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteOrderByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Order.Add(new Order() {id = 1, dateTimeOrder = "testdatetimeorder 1", personId = "testpersonid 1"});

            });
            var beforeDeleteResponse = await client.GetAsync("flowerchainapi/order/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("flowerchainapi/order/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("flowerchainapi/order/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InsertOrderReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new OrderUpsertInput
                {
                    
                    dateTimeOrder = "testdatetimeorder 1",
                    personId = "testpersonid 1"
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/order", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<OrderWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.id.Should().Be(1);
            body.dateTimeOrder.Should().Be("testdatetimeorder 1");
            body.personId.Should().Be("testpersonid 1");
            var getResponse = await client.GetAsync($"flowerchainapi/order/{body.id}");
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertOrderThrowsErrorOnEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new OrderUpsertInput
                {
                    
                    dateTimeOrder = string.Empty,
                    personId = string.Empty
                }
            };
            var createResponse = await client.PostAsync("flowerchainapi/order", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateOrderReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new OrderUpsertInput
                {
                    dateTimeOrder = "testdatetimeorder 1",
                    personId = "testpersonid 1"
                }
            };
            var patchResponse = await client.PatchAsync("flowerchainapi/order/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

}
}