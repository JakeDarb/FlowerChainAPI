using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Models.Domain;

namespace FlowerChainAPI.Repositories
{

    public interface IFlowerShopRepository
    {
        Task<IEnumerable<FlowerShop>> GetAllShops();
        Task<FlowerShop> GetOneShopById(int id);
        Task Delete(int id);
        Task<FlowerShop> Insert(int Id, string ShopName, string StreetName, string HouseNumber, string City, string PostalCode, string PhoneNumber, string Email);
        Task<FlowerShop> Update(int Id, string ShopName, string StreetName, string HouseNumber, string City, string PostalCode, string PhoneNumber, string Email);
    }
}