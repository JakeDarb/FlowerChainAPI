using System.Collections.Generic;
using System.Threading.Tasks;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
         public async Task<IActionResult> GetOrders()
         {
             _logger.LogInformation("Getting all orders");
             return  Ok(await _orders.GetAllOrdersAsync());
         }

        //Post FlowerChainAPI/Order
         [HttpPost]
         public async Task<IActionResult> Postorder(Order order)
         {
             _logger.LogInformation("Posting an order");
             return Ok(await _orders.CreateOrderAsync(order));
         }

         //Delete FlowerChainAPI/Order
         [HttpDelete]
         public async Task<IActionResult> DeleteOrder(int id)
         {
             _logger.LogInformation("Deleting an order");
             return Ok(await _orders.DeleteOrderAsync(id));
         }
       
        

         
     }
 }