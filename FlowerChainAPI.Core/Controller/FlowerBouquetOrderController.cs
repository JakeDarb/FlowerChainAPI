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
         private readonly FlowerBouquetOrderRepository _flowerbouquetorders;

         public FlowerBouquetOrderController(FlowerBouquetOrderRepository flowerbouquetorders , ILogger<FlowerBouquetOrderController> logger) {
            
            _logger = logger;
            _flowerbouquetorders = flowerbouquetorders;

        } 


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FlowerShopWebOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFlowerBouquetOrders()
        {
            _logger.LogInformation("Getting all flowerbouquetorders");
            var orders = await _flowerbouquetorders.GetAllOrders();
            return Ok(orders);
        }
            

        [HttpGet("id:length(24)", Name = "GetFlowerBouquetOrder")]
        [ProducesResponseType(typeof(FlowerShopWebOutput), StatusCodes.Status200OK)]
        public async Task<ActionResult<FlowerBouquetOrder>> FlowerBouquetOrderById(string id)
        {
            _logger.LogInformation("Getting flowershop by id", id);
            var order = await _flowerbouquetorders.GetOneOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    

        [HttpPost]
        [ProducesResponseType(typeof(FlowerBouquetOrderWebOutput),StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<FlowerBouquetOrder>> CreateFlowerBouquetOrder(FlowerBouquetOrderUpsertInput input)
        {
            
            _logger.LogInformation("Creating a flowerbouquetorder", input);
            try{
                var persistedOrder = await _flowerbouquetorders.Insert(input.flowerBouquetId, input.orderId, input.amount);
                return Created($"/orders/{persistedOrder.id}", persistedOrder.Convert());
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPatch("id:length(24)")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateFlowerBouquetOrder(string id, FlowerBouquetOrder input)
        {
            _logger.LogInformation("Updating a flowerbouquetorder", input);
            try{
                await _flowerbouquetorders.Update(id, input);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
               
        }


        [HttpDelete("id:length(24)")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteFlowerBouquetOrder(string id)
        {
            _logger.LogInformation("Deleting a flowershop", id);
            try{
                await _flowerbouquetorders.Delete(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

     }
}