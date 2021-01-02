using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Models.Domain;

namespace FlowerChainAPI.Repositories
{

    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOneOrderById(int id);
        Task Delete(int id);
        Task<Order> Insert(int Id, string DateTimeOrder, string PersonId);
        Task<Order> Update(int Id, string DateTimeOrder, string PersonId);
    }
}