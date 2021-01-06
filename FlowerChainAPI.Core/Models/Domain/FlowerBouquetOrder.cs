using System.Runtime.ExceptionServices;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowerChainAPI.Models.Domain
{
    public class FlowerBouquetOrder
    {
        [BsonId]
        
        public string id { get; set; }

        public int orderId { get; set; }

        public int flowerBouquetId { get; set; }

        public int amount { get; set; }





    }
}
