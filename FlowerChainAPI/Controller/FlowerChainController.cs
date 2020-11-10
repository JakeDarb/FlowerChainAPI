using System.Collections.Generic;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FlowerChainAPI.Controller
 {
     [Route("FlowerChainAPI/[controller]")]
     [ApiController]

     public class FlowerChainController : ControllerBase
     {
         private readonly FlowerChainContext _context;

        public FlowerChainController(FlowerChainContext context) => _context = context;
         
         
         //Get: FlowerChainAPI/FlowerBouquets
         [HttpGet]
         public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquet()
         {
             return _context.FlowerBouquet;
         }
       
        //Get: FlowerChainAPI/FlowerBouquets/n
        [HttpGet("{id}")]
        public ActionResult<FlowerBouquet> GetBouquetId(int id)
        {
            var bouquetItem = _context.FlowerBouquet.Find(id);
            
            if(bouquetItem == null){
                return NotFound();
            }else{
                return bouquetItem;
            }
        }

       //Post: FlowerChainAPI/FlowerBouquet
        [HttpPost]
        public ActionResult<FlowerBouquet> PostFlowerBouquetItem(FlowerBouquet flowerbouquet)
        {
            _context.FlowerBouquet.Add(flowerbouquet);
            _context.SaveChanges();
            
            return CreatedAtAction("GetFlowerBouquetItem", new FlowerBouquet{id= flowerbouquet.id}, flowerbouquet);
        }

        //Put: FlowerChainAPI/FlowerBouquet/n
        [HttpPut("{id}")]
        public ActionResult PutFlowerBouquetItem(int id, FlowerBouquet flowerbouquet)
        {
            if(id != flowerbouquet.id){
                return BadRequest();
            }

            _context.Entry(flowerbouquet).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }






        //Delete FlowerChainAPI/FlowerBouquet/n
        [HttpDelete("{id}")]
        public ActionResult<FlowerBouquet> DeleteFlowerBouquetItem(int id)
        {
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