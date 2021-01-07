using FlowerChainAPI.Models;
using FlowerChainAPI.Models.Domain;
using FlowerChainAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FlowerChainAPI.Controller
{
    [ApiController]
    [Route("FlowerChainAPI/[controller]")]

    public class FlowerBouquetOrderController : ControllerBase
     {

         private readonly ILogger<FlowerBouquetOrderController> _logger;
         private readonly FlowerBouquetOrderRepository _flowerbouquetorder;

         public FlowerBouquetOrderController(FlowerBouquetOrderRepository flowerbouquetorder , ILogger<FlowerBouquetOrderController> logger) {
            
            _logger = logger;
            _flowerbouquetorder = flowerbouquetorder;

        } 

        [HttpGet]
        public ActionResult<List<FlowerBouquetOrder>> Get() =>
            _flowerbouquetorder.Get();


     

         [HttpGet("id:length(24)", Name = "GetFlowerBouquetOrder")]
        public ActionResult<FlowerBouquetOrder> Get(string Id)
        {
            var order = _flowerbouquetorder.Get(Id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<FlowerBouquetOrder> Create(FlowerBouquetOrder order)
        {
            _flowerbouquetorder.Create(order);
            

            return CreatedAtRoute("GetFlowerBouquetOrder", new { Id = order.id.ToString() }, order);
        }


        [HttpPut("id:length(24)")]
        public IActionResult Update(string Id, FlowerBouquetOrder flowerBouquetOrderIn)
        {
            var order = _flowerbouquetorder.Get(Id);

            if (order == null)
            {
                return NotFound();
            }

            _flowerbouquetorder.Update(Id, flowerBouquetOrderIn);

            return NoContent();
        }

        [HttpDelete("id:length(24)")]
        public IActionResult Delete(string Id)
        {
            var order = _flowerbouquetorder.Get(Id);

            if (order == null)
            {
                return NotFound();
            }

            _flowerbouquetorder.Remove(order.id);

            return NoContent();
        }


     }






}