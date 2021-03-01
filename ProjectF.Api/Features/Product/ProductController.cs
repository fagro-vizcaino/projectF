using ProjectF.Application.Products;
using Microsoft.AspNetCore.Mvc;
using static ProjectF.Application.Products.ProductMapper;
using System.Threading.Tasks;
using ProjectF.Api.Infrastructure.Extensions;
using ProjectF.Data.Entities.Products;

namespace ProjectF.Api.Features.Product
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductCrudHandler _productOperation;

        public ProductController(ProductCrudHandler productOperation)
            => _productOperation = productOperation;

        [HttpPost]
        public ActionResult CreateProduct(CreateProductRequest request)
          => _productOperation
              .Create(request)
            .Match<ActionResult>(
                  Left: err => BadRequest(err.Message),
                  Right: p => CreatedAtRoute(nameof(GetProduct), new { p.Id }, FromEntity(p)));


        [HttpPut("{id:int}")]
        public ActionResult UpdateProduct(int id, CreateProductRequest request)
            => _productOperation
                .Update(id, request)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: p => Ok(FromEntity(p)));


        [HttpGet("{id:int}", Name = "GetProduct")]
        public Task<IActionResult> GetProduct(int id)
            => _productOperation.GetByKey(id)
                .ToActionResult();

        [HttpGet]
        public Task<IActionResult> GetProductList([FromQuery] ProductListParameters productListParameters)
            => _productOperation.GetProductList(productListParameters)
            .ToActionResult<ProductMainListDto, ProductDto>(Response);

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
           => _productOperation.Delete(id)
            .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}
