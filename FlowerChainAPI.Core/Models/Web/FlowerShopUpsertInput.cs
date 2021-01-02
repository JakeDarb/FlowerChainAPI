using System.ComponentModel.DataAnnotations;

namespace FlowerChainAPI.Models.Web
{
    // This is incoming data
    // Debatable whether or not this is a good idea; normally you should split the POST/PUT messages.
    // However, since name is the only data we can change we reuse the class everywhere.
    public class FlowerShopUpsertInput
    {
        // This is called "Model validation"
        // you can find more info @ https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1#validation-attributes
        // and https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-3.1#automatic-http-400-responses
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