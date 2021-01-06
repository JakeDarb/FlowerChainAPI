using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Models.Domain;

namespace FlowerChainAPI.Repositories
{

    public interface IFlowerBouquetOrderRepository
    {
        Task<IEnumerable<FlowerBouquetOrder>> GetAllFlowerBouquetOrders();
        Task<FlowerBouquetOrder> GetOneFlowerBouquetOrderById(int id);
        Task Delete(int id);
        Task<FlowerBouquetOrder> Insert(int OrderId, int FlowerBouquetId, int Amount);
        Task<FlowerBouquetOrder> Update(int Id, int OrderId, int FlowerBouquetId, int Amount);
    }
    
}