using ProjectF.Application.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static ProjectF.Api.Features.Product.ProductViewModel;
using static ProjectF.Data.Entities.Products.ProductMapper;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Products;
using System.Text.Json;

namespace ProjectF.Api.Features.Product
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly ProductCrudHandler _productOperation;

        public ProductController(ProductCrudHandler productOperation)
            => _productOperation = productOperation;

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel viewModel)
          => _productOperation
              .Create(viewModel.ToDto())
            .Match<ActionResult>(
                  Left: err => BadRequest(err.Message),
                  Right: p => CreatedAtRoute(nameof(GetProduct),
                      new { p.Id },
                      FromDtoToView(FromEntity(p))));


        [HttpPut("{id}")]
        public ActionResult UpdateProduct(long id, ProductViewModel viewModel)
            => _productOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: p => Ok(FromDtoToView(FromEntity(p))));


        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(long id)
            => _productOperation
                .GetByKey(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(FromDtoToView(FromEntity(c))));

        [HttpGet]
        public async Task<ActionResult> GetClients([FromQuery] ProductListParameters productListParameters)
       => (await _productOperation.GetProductList(productListParameters))
               .Match<ActionResult>(Left: err => NotFound(err.Message), Right: c => {
                           Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(c.meta));
                           return Ok(c.list);
                   });

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _productOperation.Delete(id)
            .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());
    }
}
