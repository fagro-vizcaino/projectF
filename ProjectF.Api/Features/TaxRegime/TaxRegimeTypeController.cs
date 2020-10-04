using ProjectF.Application.Taxes;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProjectF.Api.Features.TaxRegime
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class TaxRegimeTypeController : ControllerBase
    {
         private readonly TaxRegimeTypeCrudHandler _taxRegimeTypeOperation;

        public TaxRegimeTypeController(TaxRegimeTypeCrudHandler taxRegimeTypeOperation) {
            _taxRegimeTypeOperation = taxRegimeTypeOperation;
        }

        [HttpPost]
        public ActionResult CreateTaxRegimeType(TaxRegimeTypeViewModel viewModel)
            => _taxRegimeTypeOperation
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: t => Ok(TaxRegimeTypeViewModel.FromDto(t)));


        [HttpPut("{id}")]
        public ActionResult UpdateTaxRegimeType(long id, TaxRegimeTypeViewModel viewModel)
            => _taxRegimeTypeOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: tr => Ok(TaxRegimeTypeViewModel.FromDto(tr)));


        [HttpGet("{id}")]
        public IActionResult GetTaxRegimeType(long id)
            =>  _taxRegimeTypeOperation
                .Find(id)
                .Match<ActionResult>(
                    Left : err => NotFound(err.Message),
                    Right: t=> Ok(TaxRegimeTypeViewModel.FromDto(t)));


        [HttpGet]
        public ActionResult GetTaxRegimeTypes() {
            var result = _taxRegimeTypeOperation.GetAll()
                .Select(tr => TaxRegimeTypeViewModel.FromDto(tr));
            if (result.Any()) return Ok(result);

            return NotFound();
        }
    }
}
