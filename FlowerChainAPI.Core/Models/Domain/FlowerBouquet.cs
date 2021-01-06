using System;
namespace FlowerChainAPI.Models.Domain
{
    public class FlowerBouquet
    {
        public int id { get; set; }

        public String bouquetName{get;set;}

        public double price { get; set; }

        public int  amountSold { get; set; }
        
        public String description {get;set;}
    }
}