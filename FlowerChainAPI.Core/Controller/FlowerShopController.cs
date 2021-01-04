using System.Collections.Generic;
using System.Threading.Tasks;
using System.Configuration;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Models.Web;
using FlowerChainAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace FlowerChainAPI.Controller
{
    [Route("FlowerChainAPI/[controller]")]
     [ApiController]

     public class FlowerShopController : ControllerBase
     {
         
         private readonly ILogger<FlowerShopController> _logger;
         private readonly IFlowerShopRepository _shops;
         

        public FlowerShopController(IFlowerShopRepository shops , ILogger<FlowerShopController> logger) {
            
            _logger = logger;
            _shops = shops;

        } 
         
         
         
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<FlowerShopWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetAllFlowerShops()
         {
             _logger.LogInformation("Getting all flowershops");
             var shops = (await _shops.GetAllShops()).Select(x => x.Convert()).ToList();
             return  Ok(shops);
         }

         [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlowerShopWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> FlowerShopById(int id)
        {
            _logger.LogInformation("Getting flowershop by id", id);
            var shop = await _shops.GetOneShopById(id);
            return shop == null ? (IActionResult) NotFound() : Ok(shop.Convert());
        }

        [HttpPost]
        [ProducesResponseType(typeof(FlowerShopWebOutput),StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateFlowerShop(FlowerShopUpsertInput input)
        {
            _logger.LogInformation("Creating a flowershop", input);
            try{
                var persistedShop = await _shops.Insert(input.shopName, input.streetName, input.houseNumber, input.city, input.postalCode, input.phoneNumber, input.email);
                return Created($"/shops/{persistedShop.id}", persistedShop.Convert());
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            
        }

         [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateFlowerShop(int id, FlowerShopUpsertInput input)
        {
            _logger.LogInformation("Updating a flowershop", input);
            
            try
            {
                await _shops.Update(id, input.shopName, input.streetName, input.houseNumber, input.city, input.postalCode, input.phoneNumber, input.email);
                return Accepted();
            }
            catch (NotFoundException)
            {
                // we only catch the NotFoundException; only catch exceptions you explicitly want to behave different from the regular handling.
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteFlowerShop(int id)
        {
            _logger.LogInformation("Deleting a flowershop", id);
            try
            {
                await _shops.Delete(id);
               return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

         
       
        

         
     }
 }