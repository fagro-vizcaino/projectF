using System.Linq;
using System.Threading.Tasks;
using ProjectF.Application.Categories;
using ProjectF.Application.Werehouses;
using static ProjectF.Api.Features.Werehouses.WerehouseViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Werehouses {

    [Route("api/inventory/[controller]")]
    [ApiController]
    public class WerehouseController : ControllerBase {
        private readonly WerehouseCrudHandler _werehouseOperation;

        public WerehouseController(WerehouseCrudHandler werehouseOperation) {
            _werehouseOperation = werehouseOperation;
        }

        [HttpPost]
        public ActionResult CreateWerehouse(WerehouseViewModel werehouseViewmodel)
            => _werehouseOperation
                .Create(werehouseViewmodel.ToDto())
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: werehouse => Ok(FromDto(werehouse)));


        [HttpPut("{id}")]
        public ActionResult UpdateWerehouse(long id, WerehouseViewModel viewModel)
            => _werehouseOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetWerehouse(long id)
            =>  _werehouseOperation
                .Find(id)
                .Match<ActionResult>(
                    Left : err => NotFound(err.Message),
                    Right: w => Ok(FromDto(w)));


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
        //[HttpGet]

        //[HttpGet]
        //public ActionResult GetAll([FromQuery] PaginationQuery paginationQuery )
        //{
        //    var result = _categoryOperations.GetAll()
        //    return Ok();
        //}
    }
}