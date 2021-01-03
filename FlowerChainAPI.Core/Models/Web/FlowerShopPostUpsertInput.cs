using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerShopPostUpsertInput
    {
        
        [Required]
        
        public int id { get; set; }
        [StringLength(1000)]
        public string shopName { get; set; }
        public string streetName { get; set; }
        public string houseNumber { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
    }
}