using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
   
    public class FlowerBouquetOrderUpsertInput
    {
        
        [Required]
        
        public int orderId {get;set;}

        [Required]
        
        public int flowerBouquetId { get; set; }

        [Required]
        
        public int amount { get; set; }

        
        
    }

}
