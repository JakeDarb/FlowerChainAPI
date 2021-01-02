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
        Task<FlowerBouquet> GetBouquetByNameAsync(string bouquetName);
    }
}