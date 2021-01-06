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
    [ApiController]
    [Route("FlowerChainAPI/[controller]")]
     

     public class OrderController : ControllerBase
     {
         
         private readonly ILogger<OrderController> _logger;
         private readonly IOrderRepository _orders;
         

        public OrderController(IOrderRepository orders , ILogger<OrderController> logger) {
            
            _logger = logger;
            _orders = orders;

        } 
         
         
        
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<OrderWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetAllOrders()
         {
             _logger.LogInformation("Getting all orders");
             var orders = (await _orders.GetAllOrders()).Select(x => x.Convert()).ToList();
             return  Ok(orders);
         }

        
         [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> OrderById(int id)
        {
            _logger.LogInformation("Getting orders by id", id);
            try{
                var order = await _orders.GetOneOrderById(id);
                return order == null ? (IActionResult) NotFound() : Ok(order.Convert());
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(OrderWebOutput),StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateOrder(OrderUpsertInput input)
        {
            _logger.LogInformation("Creating an order", input);
            var persistedOrder = await _orders.Insert(input.dateTimeOrder, input.personId);
            return Created($"/orders/{persistedOrder.id}", persistedOrder.Convert());
        }

        
         [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpsertInput input)
        {
            _logger.LogInformation("Updating an order", input);
            
            try
            {
                await _orders.Update(id, input.dateTimeOrder, input.personId);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _logger.LogInformation("Deleting an order", id);
            try
            {
                await _orders.Delete(id);
               return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        
       
        

         
     }
 }