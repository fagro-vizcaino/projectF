using System.Linq;
using static ProjectF.Data.Entities.Taxes.TaxMapper;
using ProjectF.Application.Taxes;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Taxes
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
         private readonly TaxCrudHandler _taxOperation;

        public TaxController(TaxCrudHandler taxOperation) {
            _taxOperation = taxOperation;
        }

        [HttpPost]
        public ActionResult CreateTax(TaxViewModel viewModel)
            => _taxOperation
                .Create(viewModel.ToDto())
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: tax => Ok(TaxViewModel.FromDtoToView(FromEntity(tax))));


        [HttpPut("{id}")]
        public ActionResult UpdateTax(long id, TaxViewModel viewModel)
            => _taxOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: t => Ok(TaxViewModel.FromDtoToView(FromEntity(t))));


        [HttpGet("{id}")]
        public IActionResult GetTax(long id)
            =>  _taxOperation
                .Find(id)
                .Match<ActionResult>(
                    Left : err => NotFound(err.Message),
                    Right: tax => Ok(TaxViewModel.FromDtoToView(FromEntity(tax))));


        [HttpGet]
        public ActionResult GetTaxes() {
            var result = _taxOperation.GetAll()
                .Select(c => TaxViewModel.FromDtoToView(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _taxOperation.Delete(id)
           .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}
