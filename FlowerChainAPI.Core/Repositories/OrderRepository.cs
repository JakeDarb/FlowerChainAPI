using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return order;

            
        }

        public async Task<Order> DeleteOrderAsync(int id)
        {
            var orderItem = _context.Order.Find(id);
            
                
                _context.Order.Remove(orderItem);
                await _context.SaveChangesAsync();

                return orderItem;

            
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
           return await _context.Order.FirstOrDefaultAsync(x => x.id == id);
        }

        
    }


}