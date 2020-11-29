using System.Linq;
using ProjectF.Application.Werehouses;
using static ProjectF.Data.Entities.Warehouses.WarehouseMapper;
using static ProjectF.Api.Features.Werehouses.WarehouseViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Werehouses
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase {
        private readonly WerehouseCrudHandler _werehouseOperation;

        public WarehouseController(WerehouseCrudHandler werehouseOperation) {
            _werehouseOperation = werehouseOperation;
        }

        [HttpPost]
        public ActionResult CreateWerehouse(WarehouseViewModel werehouseViewmodel)
            => _werehouseOperation
                .Create(werehouseViewmodel.ToDto())
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: w => Ok(FromDtoToView(FromEntity(w))));


        [HttpPut("{id}")]
        public ActionResult UpdateWerehouse(long id, WarehouseViewModel viewModel)
            => _werehouseOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromDtoToView(FromEntity(c))));


        [HttpGet("{id}")]
        public IActionResult GetWerehouse(long id)
            =>  _werehouseOperation
                .Find(id)
                .Match<ActionResult>(
                    Left : err => NotFound(err.Message),
                    Right: w => Ok(FromDtoToView(FromEntity(w))));


        [HttpGet]
        public ActionResult GetWerehouses() {
            var result = _werehouseOperation.GetAll()
                .Select(c => FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _werehouseOperation.Delete(id)
           .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}