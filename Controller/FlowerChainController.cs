using System.Collections.Generic;
using FlowerChainAPI.Database;
using FlowerChainAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc;


namespace FlowerChainAPI.Controller
 {
     [Route("api/[controller]")]
     [ApiController]

     public class FlowerChainController : ControllerBase
     {
         private readonly FlowerChainContext _context;

        public FlowerChainController(FlowerChainContext context) => _context = context;
         
         
         //Get: api/FlowerBouquets
         [HttpGet]
         public ActionResult<IEnumerable<FlowerBouquet>> GetFlowerBouquet()
         {
             return _context.FlowerBouquet;
         }
        
        

         
     }
 }