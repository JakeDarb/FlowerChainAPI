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
     

     public class FlowerBouquetOrderController : ControllerBase
     {
         
         private readonly ILogger<FlowerBouquetOrderController> _logger;
         private readonly IFlowerBouquetOrderRepository _orders;
         

        public FlowerBouquetOrderController(IFlowerBouquetOrderRepository orders , ILogger<FlowerBouquetOrderController> logger) {
            
            _logger = logger;
            _orders = orders;

        } 
         
         
        
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<FlowerBouquetOrderWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetAllOrders()
         {
             _logger.LogInformation("Getting all flowerbouquetorders");
             var orders = (await _orders.GetAllFlowerBouquetOrders()).Select(x => x.Convert()).ToList();
             return  Ok(orders);
         }

        
         [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlowerBouquetOrderWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> FlowerBouquetOrderById(int id)
        {
            _logger.LogInformation("Getting flowerbouquetorders by id", id);
            try{
                var order = await _orders.GetOneFlowerBouquetOrderById(id);
                return order == null ? (IActionResult) NotFound() : Ok(order.Convert());
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            
        }

        
        [HttpPost]
        [ProducesResponseType(typeof(FlowerBouquetOrderWebOutput),StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateFlowerBouquetOrder(FlowerBouquetOrderUpsertInput input)
        {
            _logger.LogInformation("Creating an flowerbouquetorder", input);
            try{
                var persistedOrder = await _orders.Insert(input.orderId, input.flowerBouquetId, input.amount);
            return Created($"/flowerbouquetorders/{persistedOrder.id}", persistedOrder.Convert());
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
        public async Task<IActionResult> UpdateFlowerBouquetOrder(int id, FlowerBouquetOrderUpsertInput input)
        {
            _logger.LogInformation("Updating an flowerbouquetorder", input);
            
            try
            {
                await _orders.Update(id, input.orderId, input.flowerBouquetId, input.amount);
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
        public async Task<IActionResult> DeleteFlowerBouquetOrder(int id)
        {
            _logger.LogInformation("Deleting an flowerbouquetorder", id);
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