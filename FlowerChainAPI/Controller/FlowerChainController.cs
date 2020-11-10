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

     public class FlowerChainController : ControllerBase
     {
         private readonly FlowerChainContext _context;
         private readonly ILogger<FlowerChainContext> _logger;

        public FlowerChainController(FlowerChainContext context, ILogger<FlowerChainContext> logger) {
            _context = context;
            _logger = logger;

        } 
         
         
         //Get: FlowerChainAPI/FlowerBouquets
         [HttpGet]
         public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquet()
         {
             _logger.LogInformation("Getting all flowerbouquets");
             return _context.FlowerBouquet;
         }
       
        //Get: FlowerChainAPI/FlowerBouquets/n
        [HttpGet("{id}")]
        public ActionResult<FlowerBouquet> GetBouquetId(int id)
        {
            _logger.LogInformation("Getting flowerbouqutes by id", id);
            var bouquetItem = _context.FlowerBouquet.Find(id);
            
            if(bouquetItem == null){
                return NotFound();
            }else{
                return bouquetItem;
            }
        }

       //Post: FlowerChainAPI/FlowerBouquet
        [HttpPost]
        public ActionResult<FlowerBouquet> PostFlowerBouquetItem(FlowerBouquet input)
        {
            _logger.LogInformation("Creating a flowerbouquet", input);

            _context.FlowerBouquet.Add(input);
            _context.SaveChanges();
            
            return CreatedAtAction("GetFlowerBouquetItem", new FlowerBouquet{id= input.id}, input);
        }

        //Put: FlowerChainAPI/FlowerBouquet/n
        [HttpPut("{id}")]
        public ActionResult PutFlowerBouquetItem(int id, FlowerBouquet input)
        {
            _logger.LogInformation("Updating a flowerbouquet", input);
            if(id != input.id){
                return BadRequest();
            }

            _context.Entry(input).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }






        //Delete FlowerChainAPI/FlowerBouquet/n
        [HttpDelete("{id}")]
        public ActionResult<FlowerBouquet> DeleteFlowerBouquetItem(int id)
        {
            _logger.LogInformation("Deleting a flowerbouquet", id);

            var FlowerBouquetItem = _context.FlowerBouquet.Find(id);
            if(FlowerBouquetItem == null){
                return NotFound();
            }else{
                
                _context.FlowerBouquet.Remove(FlowerBouquetItem);
                _context.SaveChanges();

                return FlowerBouquetItem;

            }
        }
         
     }
 }