using System.Linq;
using ProjectF.Application.Categories;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Category
{

    [Route("api/inventory/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryCrudHandler _categoryOperations;

        public CategoryController(CategoryCrudHandler categoryOperations)
        {
            _categoryOperations = categoryOperations;
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel viewModel)
            => _categoryOperations
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: category => Ok(category));


        [HttpPut("{id}")]
        public ActionResult UpdateCategory(long id, CategoryViewModel viewModel)
            => _categoryOperations
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(CategoryViewModel.FromDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetCategory(long id)
            => _categoryOperations
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(CategoryViewModel.FromDto(c)));


        [HttpGet]
        public ActionResult GetCategories()
        {
            var result = _categoryOperations.GetAll()
                .Select(c => CategoryViewModel.FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) 
            => _categoryOperations.Delete(id)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: c => NoContent());
        //[HttpGet]
        //public ActionResult GetAll([FromQuery] PaginationQuery paginationQuery )
        //{
        //    var result = _categoryOperations.GetAll()
        //    return Ok();
        //}
    }
}