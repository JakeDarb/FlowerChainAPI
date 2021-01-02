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

     public class OrderController : ControllerBase
     {
         
         private readonly ILogger<FlowerChainContext> _logger;
         private readonly IOrderRepository _orders;
         

        public OrderController(IOrderRepository orders , ILogger<FlowerChainContext> logger) {
            
            _logger = logger;
            _orders = orders;

        } 
         
         
         //Get FlowerChainAPI/Order
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<OrderWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetOrders()
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
            var persistedOrder = await _orders.Insert(input.id, input.dateTimeOrder, input.personId);
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
                await _orders.Update(input.id, input.dateTimeOrder, input.personId);
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
        public async Task<IActionResult> DeleteOder(int id)
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