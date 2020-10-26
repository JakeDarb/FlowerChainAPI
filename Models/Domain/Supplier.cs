using System;
namespace FlowerChainAPI.Models.Domain
{
    public class Supplier
    {
        public int Id { get; set; }
        public String streetName { get; set; }
        public String postalCode { get; set; }
        public String city { get; set; }
        public String shopName { get; set; }
        public String phoneNumber { get; set; }
        public String email { get; set; }

        public String supplierId { get; set; }
    }
}