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

     public class OrderController : ControllerBase
     {
         private readonly FlowerChainContext _context;
         private readonly ILogger<FlowerChainContext> _logger;

        public OrderController(FlowerChainContext context, ILogger<FlowerChainContext> logger) {
            _context = context;
            _logger = logger;

        } 
         
         
         
       
       

       //Post: FlowerChainAPI/order
        [HttpPost]
        public ActionResult<Order> PostOrderItem(Order input)
        {
            _logger.LogInformation("Creating a order", input);

            _context.Order.Add(input);
            _context.SaveChanges();
            
            return CreatedAtAction("GetOrderItem", new Order{id= input.id}, input);
        }




        //Delete FlowerChainAPI/Order/n
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrderItem(int id)
        {
            _logger.LogInformation("Deleting a order", id);

            var orderItem = _context.Order.Find(id);
            if(orderItem == null){
                return NotFound();
            }else{
                
                _context.Order.Remove(orderItem);
                _context.SaveChanges();

                return orderItem;

            }
        }
         
     }
 }