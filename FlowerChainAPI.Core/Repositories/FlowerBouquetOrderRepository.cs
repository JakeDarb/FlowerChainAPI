using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Database;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading.Tasks;

namespace FlowerChainAPI.Repositories
{
    public class FlowerBouquetOrderRepository 
    {
        private readonly IMongoCollection<FlowerBouquetOrder> _flowerbouquetorder;

        public FlowerBouquetOrderRepository(IFlowerChainDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _flowerbouquetorder = database.GetCollection<FlowerBouquetOrder>(settings.FlowerBouquetOrderCollectionName);
        }

        public async Task<IEnumerable<FlowerBouquetOrder>> GetAllOrders()
        {
           return await _flowerbouquetorder.Find(FlowerBouquetOrder => true).ToListAsync();
        }

        public async Task<FlowerBouquetOrder> GetOneOrderById(string Id)
        {
            return await _flowerbouquetorder.Find<FlowerBouquetOrder>(FlowerBouquetOrder => FlowerBouquetOrder.id == Id).FirstOrDefaultAsync();
        }
        
        public async Task<FlowerBouquetOrder> Insert(int FlowerBouquetId, int OrderId, int Amount)
        {
            var order = new FlowerBouquetOrder
            {
                
                flowerBouquetId = FlowerBouquetId,
                orderId = OrderId,
                amount = Amount
            };
            
            await _flowerbouquetorder.InsertOneAsync(order);
            return order;
        }

        
        public async Task Update(string Id, FlowerBouquetOrder flowerBouquetOrderIn)
        {
            await _flowerbouquetorder.ReplaceOneAsync(FlowerBouquetOrder => FlowerBouquetOrder.id == Id, flowerBouquetOrderIn);

        }
        
        public async Task Delete(string Id)
        {
            await _flowerbouquetorder.DeleteOneAsync(FlowerBouquetOrder => FlowerBouquetOrder.id == Id);
            
        }

        
    }
}