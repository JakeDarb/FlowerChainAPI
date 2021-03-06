using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Models.Web;

namespace FlowerChainAPI.Models
{
    public static class Mappers
    {
        public static FlowerBouquetWebOutput Convert(this FlowerBouquet input)
        {
            return new FlowerBouquetWebOutput(input.id, input.bouquetName, input.price, input.amountSold, input.description);
        }

        public static FlowerShopWebOutput Convert(this FlowerShop input)
        {
            return new FlowerShopWebOutput(input.id, input.shopName, input.streetName, input.houseNumber, input.city, input.postalCode, input.phoneNumber,input.email);
        }

        public static FlowerBouquetOrderWebOutput Convert(this FlowerBouquetOrder input)
        {
            return new FlowerBouquetOrderWebOutput(input.id, input.orderId, input.flowerBouquetId, input.amount);
        }

        

       

        

       
    }
}