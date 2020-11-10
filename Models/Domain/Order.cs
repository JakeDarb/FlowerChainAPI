using System;
namespace FlowerChainAPI.Models.Domain
{
    public class Order
    {
        public int id { get; set; }
        public DateTime datTimeOrder { get; set; }

        public String personId{ get; set; }
    }
}