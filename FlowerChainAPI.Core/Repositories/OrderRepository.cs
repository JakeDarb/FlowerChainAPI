using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Models;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerChainAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly FlowerChainContext _context;

        public OrderRepository(FlowerChainContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }
        public async Task<Order> GetOneOrderById(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                throw new NotFoundException();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Insert(String DateTimeOrder, string PersonId)
        {
            var order = new Order
            {
            
                dateTimeOrder = DateTimeOrder,
                personId = PersonId
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> Update(int Id, string DateTimeOrder, string PersonId)
        {
            var order = await _context.Order.FindAsync(Id);
            if (order == null)
            {
                throw new NotFoundException();
            }

            order.id = Id;
            order.dateTimeOrder = DateTimeOrder;
            order.personId = PersonId;
            await _context.SaveChangesAsync();
            return order;
        }


}
}