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
     

     public class FlowerBouquetController : ControllerBase
     {
         
         private readonly ILogger<FlowerBouquetController> _logger;
         private readonly IFlowerBouquetRepository _bouquets;
         

        public FlowerBouquetController(IFlowerBouquetRepository bouquets , ILogger<FlowerBouquetController> logger) {
            
            _logger = logger;
            _bouquets = bouquets;

        } 
         
         //Get all flower bouquets from the database
         [HttpGet]
         [ProducesResponseType(typeof(IEnumerable<FlowerBouquetWebOutput>), StatusCodes.Status200OK)]
         public async Task<IActionResult> GetAllBouquets()
         {
             
             _logger.LogInformation("Getting all flowerbouquets");
             var flowerbouquets = (await _bouquets.GetAllBouquets()).Select(x => x.Convert()).ToList();
             return  Ok(flowerbouquets);
         }

        // Get a flower bouquet by using an ID
         [HttpGet("{id}")]
        [ProducesResponseType(typeof(FlowerBouquetWebOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> FlowerBouquetById(int id)
        {
            _logger.LogInformation("Getting flowerbouquet by id", id);
            var bouquet = await _bouquets.GetOneBouquetById(id);
            return bouquet == null ? (IActionResult) NotFound() : Ok(bouquet.Convert());
        }

        // Creating a new flower bouquet in the database
        [HttpPost]
        [ProducesResponseType(typeof(FlowerBouquetWebOutput),StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateFlowerBouquet(FlowerBouquetUpsertInput input)
        {
            _logger.LogInformation("Creating a flowerbouquet", input);
            try{
                var persistedBouquet = await _bouquets.Insert(input.bouquetName, input.price, input.amountSold, input.description);
            return Created($"/bouquets/{persistedBouquet.id}", persistedBouquet.Convert());
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            
        }

        // Update a flower bouquet by using an ID
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateFlowerBouquet(int id, FlowerBouquetUpsertInput input)
        {
            _logger.LogInformation("Updating a flowerbouquet", input);
            
            try
            {
                await _bouquets.Update(id, input.bouquetName, input.price, input.amountSold, input.description);
                return Accepted();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // Delete a flower bouquet by using an ID
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteFlowerBouquet(int id)
        {
            _logger.LogInformation("Deleting a flowerbouquet", id);
            try
            {
                await _bouquets.Delete(id);
               return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
         
     }
 }