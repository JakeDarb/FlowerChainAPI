using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace FlowerChainAPI.Controller
 {
     [Route("api/[controller]")]
     [ApiController]

     public class FlowerChainController : ControllerBase
     {
         //Get: api/controller
         [HttpGet]
         public ActionResult<IEnumerable<string>> GetString()
         {
             return new string[]  {"seppe", "suckt", "in", "csgo"};
         }
     }
 }