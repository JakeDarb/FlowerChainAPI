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

     public class FlowerBouquetController : ControllerBase
     {
         
         private readonly ILogger<FlowerChainContext> _logger;
         private readonly IFlowerBouquetRepository _bouquets;
         

        public FlowerBouquetController(IFlowerBouquetRepository bouquets , ILogger<FlowerChainContext> logger) {
            
            _logger = logger;
            _bouquets = bouquets;

        } 
         
         
         
         [HttpGet]
         public async Task<IActionResult> GetFlowerBouquets()
         {
             _logger.LogInformation("Getting all flowerbouquets");
             return  Ok(await _bouquets.GetAllBouquetsAsync());
         }
       
        

         
     }
 }