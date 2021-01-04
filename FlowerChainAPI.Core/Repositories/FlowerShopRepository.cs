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
    public class FlowerShopRepository : IFlowerShopRepository
    {

        private readonly FlowerChainContext _context;

        public FlowerShopRepository(FlowerChainContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FlowerShop>> GetAllShops()
        {
            return await _context.FlowerShop.ToListAsync();
        }

        public async Task<FlowerShop> GetOneShopById(int id)
        {
            return await _context.FlowerShop.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var shop = await _context.FlowerShop.FindAsync(id);
            if (shop == null)
            {
                throw new NotFoundException();
            }

            _context.FlowerShop.Remove(shop);
            await _context.SaveChangesAsync();
        }

        public async Task<FlowerShop> Insert( string ShopName, string StreetName, string HouseNumber, string City, string PostalCode, string PhoneNumber, string Email)
        {
            var shop = new FlowerShop
            {
                
                shopName = ShopName,
                streetName = StreetName,
                houseNumber = HouseNumber,
                city = City,
                postalCode = PostalCode,
                phoneNumber = PhoneNumber,
                email = Email
            };
            
            await _context.FlowerShop.AddAsync(shop);
            await _context.SaveChangesAsync();
            return shop;
        }

        public async Task<FlowerShop> Update(int Id, string ShopName, string StreetName, string HouseNumber, string City, string PostalCode, string PhoneNumber, string Email)
        {
            var shop = await _context.FlowerShop.FindAsync(Id);
            if (shop == null)
            {
                throw new NotFoundException();
            }

            shop.id = Id;
            shop.shopName = ShopName;
            shop.streetName = StreetName;
            shop.houseNumber = HouseNumber;
            shop.city = City;
            shop.postalCode = PostalCode;
            shop.phoneNumber = PhoneNumber;
            shop.email = Email;
            
            await _context.SaveChangesAsync();
            return shop;
        }
    }


}