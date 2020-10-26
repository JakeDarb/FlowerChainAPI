using System.Collections.Generic;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;


namespace FlowerChainAPI.Controller
 {
     [Route("api/[controller]")]
     [ApiController]

     public class FlowerChainController : ControllerBase
     {
         private readonly FlowerChainContext _context;

        public FlowerChainController(FlowerChainContext context) => _context = context;
         
         
         //Get: api/flowerchain
         [HttpGet]
         public ActionResult<IEnumerable<FlowerShop>> GetFlowerShop()
         {
             return _context.FlowerShop;
         }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderId(int id)
        {
            var OrderItem = _context.Order.Find(id);
            
            if(OrderItem == null){
                return NotFound();
            }else{
                return OrderItem;
            }
        }

         
     }
 }