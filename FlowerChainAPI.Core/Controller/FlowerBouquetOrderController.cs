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


     

         [HttpGet("{id:length(24)}", Name = "GetFlowerBouquetOrder")]
        public ActionResult<FlowerBouquetOrder> Get(string id)
        {
            var order = _flowerbouquetorder.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public ActionResult<FlowerBouquetOrderRepository> Create(FlowerBouquetOrder order)
        {
            _flowerbouquetorder.Create(order);

            return CreatedAtRoute("GetFlowerBouquetOrder", new { id = order.id.ToString() }, order);
        }


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, FlowerBouquetOrder flowerBouquetOrderIn)
        {
            var order = _flowerbouquetorder.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            _flowerbouquetorder.Update(id, flowerBouquetOrderIn);

            return NoContent();
        }


     }






}