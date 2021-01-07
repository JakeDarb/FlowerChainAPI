using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Database;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        public List<FlowerBouquetOrder> Get() =>
            _flowerbouquetorder.Find(FlowerBouquetOrder => true).ToList();

        public FlowerBouquetOrder Get(string Id) =>
            _flowerbouquetorder.Find<FlowerBouquetOrder>(FlowerBouquetOrder => FlowerBouquetOrder.id == Id).FirstOrDefault();

        public FlowerBouquetOrder Create(FlowerBouquetOrder flowerbouquetorder)
        {
            _flowerbouquetorder.InsertOne(flowerbouquetorder);
            return flowerbouquetorder;
        }

        public void Update(string Id, FlowerBouquetOrder flowerBouquetOrderIn) =>
            _flowerbouquetorder.ReplaceOne(FlowerBouquetOrder => FlowerBouquetOrder.id == Id, flowerBouquetOrderIn);

        public void Remove(FlowerBouquetOrder flowerBouquetOrderIn) =>
            _flowerbouquetorder.DeleteOne(FlowerBouquetOrder => FlowerBouquetOrder.id == flowerBouquetOrderIn.id);

        public void Remove(string Id) => 
            _flowerbouquetorder.DeleteOne(FlowerBouquetOrder => FlowerBouquetOrder.id == Id);
    }
}