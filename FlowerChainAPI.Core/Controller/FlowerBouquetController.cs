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

     public class FlowerBouquetController : ControllerBase
     {
         
         private readonly ILogger<FlowerBouquetController> _logger;
         private readonly IFlowerBouquetRepository _bouquets;
         

        public FlowerBouquetController(IFlowerBouquetRepository bouquets , ILogger<FlowerBouquetController> logger) {
            
            _logger = logger;
            _bouquets = bouquets;

        } 
         
         
         
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<FlowerBouquetWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetAllBouquets()
         {
             
             _logger.LogInformation("Getting all flowerbouquets");
             var flowerbouquets = (await _bouquets.GetAllBouquets()).Select(x => x.Convert()).ToList();
             return  Ok(flowerbouquets);
         }

        
         [HttpGet("{bouquetName}")]
         public async Task<IActionResult> GetblouquetByName(string bouquetName)
         {
             _logger.LogInformation("Getting a flowerbouquet by Name");
             return Ok(await _bouquets.GetBouquetByNameAsync(bouquetName));
         }

       
        

         
     }
 }