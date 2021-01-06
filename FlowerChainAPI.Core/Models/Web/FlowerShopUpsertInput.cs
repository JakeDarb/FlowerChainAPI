using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
    
    public class FlowerShopUpsertInput
    {
        
        [Required]
        [StringLength(1000)]
        public string shopName { get; set; }

        [Required]
        [StringLength(1000)]
        public string streetName { get; set; }

        [Required]
        [StringLength(1000)]
        public string houseNumber { get; set; }

        [Required]
        [StringLength(1000)]
        public string city { get; set; }

        [Required]
        [StringLength(1000)]
        public string postalCode { get; set; }

        [Required]
        [StringLength(1000)]
        public string phoneNumber { get; set; }

        [Required]
        [StringLength(1000)]
        public string email { get; set; }

        
    }
}