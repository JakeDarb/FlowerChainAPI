using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
    
    public class OrderPostUpsertInput
    {
        
        [Required]
        
        public int id { get; set; }
        
        [StringLength(1000)]
        public string dateTimeOrder { get; set; }
        public string personId{ get; set; }
    }
}