using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
    
    public class OrderUpsertInput
    {
        
        [Required]
        
        
        
        [StringLength(1000)]
        public string dateTimeOrder { get; set; }
        public string personId{ get; set; }
    }
}