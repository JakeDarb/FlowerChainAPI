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
    public class FlowerShopRepository : IFlowerShopRepository
    {

        private readonly FlowerChainContext _context;

        public FlowerShopRepository(FlowerChainContext context)
        {
            _context = context;
        }

        public async Task<List<FlowerShop>> GetAllShopsAsync()
        {
            return await _context.FlowerShop.ToListAsync();
        }

        public async Task<FlowerShop> GetShopByNameAsync(string shopName)
        {
           return await _context.FlowerShop.FirstOrDefaultAsync(x => x.shopName == shopName);
        }
    }


}