using System.Linq;
using static ProjectF.Data.Entities.Taxes.TaxMapper;
using ProjectF.Application.Taxes;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Data.Entities.Taxes;

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
        public ActionResult CreateTax(TaxDto dto)
            => _taxOperation
                .Create(dto)
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: tax => CreatedAtRoute(nameof(GetTax), new {tax.Id}, FromEntity(tax)));


        [HttpPut("{id}")]
        public ActionResult UpdateTax(long id, TaxDto dto)
            => _taxOperation
                .Update(id, dto)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: t => Ok(FromEntity(t)));


        [HttpGet("{id}", Name ="GetTax")]
        public IActionResult GetTax(long id)
            =>  _taxOperation
                .Find(id)
                .Match<ActionResult>(
                    Left : err => NotFound(err.Message),
                    Right: tax => Ok(FromEntity(tax)));


        [HttpGet]
        public ActionResult GetTaxes() {
            var result = _taxOperation.GetAll();
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
