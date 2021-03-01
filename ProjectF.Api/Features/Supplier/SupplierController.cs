using System.Linq;
using ProjectF.Application.Suppliers;
using static ProjectF.Application.Suppliers.SupplierMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Data.Entities.Suppliers;

namespace ProjectF.Api.Features.Supplier
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class SupplierController : Controller
    {
        private readonly SupplierCrudHandler _supplierOperation;

        public SupplierController(SupplierCrudHandler supplierOperation)
        {
            _supplierOperation = supplierOperation;
        }

        [HttpPost]
        public ActionResult CreateSupplier(SupplierDto dto)
            => _supplierOperation
            .Create(dto)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: s => Ok(FromEntity(s)));

        [HttpPut("{id}")]
        public ActionResult UpdateSupplier(long id, SupplierDto dto)
            => _supplierOperation
            .Update(id, dto)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: c => Ok(FromEntity(c)));

        [HttpGet("{id}")]
        public IActionResult GetSupplier(long id)
            => _supplierOperation
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: cat => Ok(FromEntity(cat)));

        [HttpGet]
        public ActionResult GetSupplier()
        {
            var result = _supplierOperation.GetAll();
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
            => _supplierOperation.Delete(id)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: c => NoContent());
    }
}
