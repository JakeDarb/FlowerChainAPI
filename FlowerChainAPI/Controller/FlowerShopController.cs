using System.Collections.Generic;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlowerChainAPI.Controller
 {
     [Route("FlowerChainAPI/[controller]")]
     [ApiController]

     public class FlowerShopController : ControllerBase
     {
         private readonly FlowerChainContext _context;
         private readonly ILogger<FlowerChainContext> _logger;

        public FlowerShopController(FlowerChainContext context, ILogger<FlowerChainContext> logger) {
            _context = context;
            _logger = logger;

        } 
         
         
         //Get: FlowerChainAPI/FlowerShop
         [HttpGet]
         public ActionResult<IEnumerable<FlowerShop>> GetFlowerShop()
         {
             _logger.LogInformation("Getting all flowershops");
             return _context.FlowerShop;
         }
       
        //Get: FlowerChainAPI/FlowerShop/n
        [HttpGet("{id}")]
        public ActionResult<FlowerShop> GetFlowerShopId(int id)
        {
            _logger.LogInformation("Getting flowershop by id", id);
            var shopItem = _context.FlowerShop.Find(id);
            
            if(shopItem == null){
                return NotFound();
            }else{
                return shopItem;
            }
        }

       //Post: FlowerChainAPI/FlowerShop
        [HttpPost]
        public ActionResult<FlowerShop> PostFlowerShopItem(FlowerShop input)
        {
            _logger.LogInformation("Creating a flowershop", input);

            _context.FlowerShop.Add(input);
            _context.SaveChanges();
            
            return CreatedAtAction("GetFlowerShopItem", new FlowerShop{id= input.id}, input);
        }

        //Put: FlowerChainAPI/FlowerShop/n
        [HttpPut("{id}")]
        public ActionResult PutFlowerShopItem(int id, FlowerShop input)
        {
            _logger.LogInformation("Updating a flowershop", input);
            if(id != input.id){
                return BadRequest();
            }

            _context.Entry(input).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }






        //Delete FlowerChainAPI/FlowerShop/n
        [HttpDelete("{id}")]
        public ActionResult<FlowerShop> DeleteFlowershopItem(int id)
        {
            _logger.LogInformation("Deleting a flowershop", id);

            var FlowerShopItem = _context.FlowerShop.Find(id);
            if(FlowerShopItem == null){
                return NotFound();
            }else{
                
                _context.FlowerShop.Remove(FlowerShopItem);
                _context.SaveChanges();

                return FlowerShopItem;

            }
        }
         
     }
 }