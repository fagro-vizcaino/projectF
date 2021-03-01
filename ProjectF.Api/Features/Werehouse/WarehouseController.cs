using System.Linq;
using ProjectF.Application.Warehouses;
using static ProjectF.Application.Warehouses.WarehouseMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Api.Features.Werehouses
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseCrudHandler _werehouseOperation;

        public WarehouseController(WarehouseCrudHandler werehouseOperation)
        {
            _werehouseOperation = werehouseOperation;
        }

        [HttpPost]
        public ActionResult CreateWerehouse(WarehouseDto dto)
            => _werehouseOperation
                .Create(dto)
                .Match<ActionResult>(Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetWarehouse),
                        new { id = dto.Id }, dto));


        [HttpPut("{id}")]
        public ActionResult UpdateWerehouse(long id, WarehouseDto dto)
            => _werehouseOperation
                .Update(id, dto)
                 .Match<ActionResult>(Left: err => BadRequest(err.Message),
                        Right: pt => CreatedAtRoute(nameof(GetWarehouse),
                            new { id = pt.Id }, dto));


        [HttpGet("{id}", Name = "GetWarehouse")]
        public IActionResult GetWarehouse(long id)
            => _werehouseOperation
                .Find(id)
                .Match<ActionResult>(Left: err => NotFound(err.Message),
                    Right: w => Ok(FromEntity(w)));


        [HttpGet]
        public ActionResult GetWerehouses()
        {
            var result = _werehouseOperation.GetAll()
                .Select(c => c);
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _werehouseOperation.Delete(id)
           .Match<ActionResult>(Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}