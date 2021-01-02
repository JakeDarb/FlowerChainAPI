using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerChainAPI.Repositories
{
    public class FlowerBouquetRepository : IFlowerBouquetRepository
    {

        private readonly FlowerChainContext _context;

        public FlowerBouquetRepository(FlowerChainContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlowerBouquet>> GetAllBouquets()
        {
            return await _context.FlowerBouquet.ToListAsync();
        }

       public async Task<FlowerBouquet> GetOneBouquetById(int id)
        {
            return await _context.FlowerBouquet.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var bouquet = await _context.FlowerBouquet.FindAsync(id);
            if (bouquet == null)
            {
                throw new NotFoundException();
            }

            _context.FlowerBouquet.Remove(bouquet);
            await _context.SaveChangesAsync();
        }

        public async Task<FlowerBouquet> Insert(int Id, string BouquetName, double Price, int AmountSold, string Description)
        {
            var bouquet = new FlowerBouquet
            {
                id = Id,
                bouquetName = BouquetName,
                price = Price,
                amountSold = AmountSold,
                description = Description
            };
            await _context.FlowerBouquet.AddAsync(bouquet);
            await _context.SaveChangesAsync();
            return bouquet;
        }

        public async Task<FlowerBouquet> Update(int Id, string BouquetName, double Price, int AmountSold, string Description)
        {
            var bouquet = await _context.FlowerBouquet.FindAsync(Id);
            if (bouquet == null)
            {
                throw new NotFoundException();
            }

                bouquet.id = Id;
                bouquet.bouquetName = BouquetName;
                bouquet.price = Price;
                bouquet.amountSold = AmountSold;
                bouquet.description = Description;

            await _context.SaveChangesAsync();
            return bouquet;
        }
    }


}