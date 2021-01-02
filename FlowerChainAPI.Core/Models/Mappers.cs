using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Models.Web;

namespace FlowerChainAPI.Models
{
    public static class Mappers
    {
        // more extension method fun: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        public static FlowerBouquetWebOutput Convert(this FlowerBouquet input)
        {
            return new FlowerBouquetWebOutput(input.id, input.bouquetName, input.price, input.amountSold, input.description);
        }

       
    }
}