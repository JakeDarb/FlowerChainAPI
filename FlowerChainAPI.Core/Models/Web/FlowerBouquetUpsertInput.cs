using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
   
    public class FlowerBouquetUpsertInput
    {
        
        [Required]
        [StringLength(1000)]
        public string bouquetName{get;set;}

        [Required]
        
        public double price { get; set; }

        [Required]
        
        public int  amountSold { get; set; }

        [Required]
        [StringLength(1000)]
        public string description {get;set;}
    }
    }
