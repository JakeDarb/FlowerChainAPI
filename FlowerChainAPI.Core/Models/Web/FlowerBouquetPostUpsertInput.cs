using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
   
    public class FlowerBouquetPostUpsertInput
    {
        
        [Required]
        
        public int id { get; set; }

        [StringLength(1000)]
        public string bouquetName{get;set;}
        public double price { get; set; }
        public int  amountSold { get; set; }
        public string description {get;set;}
    }
    }
