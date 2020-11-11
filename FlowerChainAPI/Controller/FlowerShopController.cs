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

     public class FlowerShopController : ControllerBase
     {
         
         private readonly ILogger<FlowerChainContext> _logger;
         private readonly IFlowerShopRepository _shops;
         

        public FlowerShopController(IFlowerShopRepository shops , ILogger<FlowerChainContext> logger) {
            
            _logger = logger;
            _shops = shops;

        } 
         
         
         
         [HttpGet]
         public async Task<IActionResult> GetFlowerShops()
         {
             _logger.LogInformation("Getting all flowershops");
             return  Ok(await _shops.GetAllShopsAsync());
         }
       
        

         
     }
 }