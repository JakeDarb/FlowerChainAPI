using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Models.Domain;

namespace FlowerChainAPI.Repositories
{

    public interface IFlowerBouquetRepository
    {
        Task<IEnumerable<FlowerBouquet>> GetAllBouquets();
        Task<FlowerBouquet> GetOneBouquetById(int id);
        Task Delete(int id);
        Task<FlowerBouquet> Insert(string BouquetName, double Price, int AmountSold, string Description);
        Task<FlowerBouquet> Update(int Id, string BouquetName, double Price, int AmountSold, string Description);
    }
    
}