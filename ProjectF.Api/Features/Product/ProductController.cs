using ProjectF.Application.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ProjectF.Api.Features.Product.ProductViewModel;

namespace ProjectF.Api.Features.Product
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly ProductCrudHandler _productOperation;

        public ProductController(ProductCrudHandler productOperation)
        {
            _productOperation = productOperation;
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel viewModel)
          => _productOperation
              .Create(viewModel.ToDto())
            .Match<ActionResult>(
                  Left: err => BadRequest(err.Message),
                  Right: p => CreatedAtRoute(nameof(GetProduct),
                      new { p.Id },
                      FromDto(_productOperation.EntityToDto(p))));

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(long id, ProductViewModel viewModel)
            => _productOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: p => Ok(FromDto(_productOperation.EntityToDto(p))));

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(long id)
            => _productOperation
                .GetByKey(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(FromDto(_productOperation.EntityToDto(c))));

        [HttpGet]
        public ActionResult GetProducts()
        {
            var result = _productOperation.GetAll()
                .Select(c => FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _productOperation.Delete(id)
            .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}
