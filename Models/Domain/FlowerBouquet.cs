using System;
namespace FlowerChainAPI.Models.Domain
{
    public class FlowerBouquet
    {
        public int Id { get; set; }
        public String bouquetName{get;set;}
        public int  amountSold { get; set; }
    }
}