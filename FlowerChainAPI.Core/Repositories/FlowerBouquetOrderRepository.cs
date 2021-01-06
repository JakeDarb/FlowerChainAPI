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
    public class FlowerBouquetOrderRepository : IFlowerBouquetOrderRepository
    {

        private readonly FlowerChainContext _context;

        public FlowerBouquetOrderRepository(FlowerChainContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlowerBouquetOrder>> GetAllFlowerBouquetOrders()
        {
            return await _context.FlowerBouquetOrder.ToListAsync();
        }
        public async Task<FlowerBouquetOrder> GetOneFlowerBouquetOrderById(int id)
        {
            return await _context.FlowerBouquetOrder.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var flowerbouquetorder = await _context.FlowerBouquetOrder.FindAsync(id);
            if (flowerbouquetorder == null)
            {
                throw new NotFoundException();
            }

            _context.FlowerBouquetOrder.Remove(flowerbouquetorder);
            await _context.SaveChangesAsync();
        }

        public async Task<FlowerBouquetOrder> Insert(int OrderId, int FlowerBouquetId, int Amount)
        {
            var flowerboquetorder = new FlowerBouquetOrder
            {
            
                orderId = OrderId,
                flowerBouquetId = FlowerBouquetId,
                amount = Amount
            };
            await _context.FlowerBouquetOrder.AddAsync(flowerboquetorder);
            await _context.SaveChangesAsync();
            return flowerboquetorder;
        }

        public async Task<FlowerBouquetOrder> Update(int Id, int OrderId, int FlowerBouquetId, int Amount)
        {
            var flowerbouquetorder = await _context.FlowerBouquetOrder.FindAsync(Id);
            if (flowerbouquetorder == null)
            {
                throw new NotFoundException();
            }

            flowerbouquetorder.id = Id;
            flowerbouquetorder.orderId = OrderId;
            flowerbouquetorder.flowerBouquetId = FlowerBouquetId;
            flowerbouquetorder.amount = Amount;
            await _context.SaveChangesAsync();
            return flowerbouquetorder;
        }

        

        

       
    }
}