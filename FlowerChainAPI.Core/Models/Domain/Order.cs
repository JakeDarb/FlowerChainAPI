using System;
namespace FlowerChainAPI.Models.Domain
{
    public class Order
    {
        public int id { get; set; }
        public String dateTimeOrder { get; set; }

        public String personId{ get; set; }
    }
}