using System.Linq;
using ProjectF.Application.Categories;
using static ProjectF.Data.Entities.Categories.CategoryMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectF.Data.Entities.Categories;

namespace ProjectF.Api.Features.Categories
{

    [Route("api/config/[controller]"), Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryCrudHandler _categoryOperations;

        public CategoryController(CategoryCrudHandler categoryOperations)
        {
            _categoryOperations = categoryOperations;
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoryDto dto)
            => _categoryOperations
                .Create(dto)
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: m => CreatedAtRoute(nameof(GetCategory), new { id = m.Id }, m));



        [HttpPut("{id}")]
        public ActionResult UpdateCategory(long id, CategoryDto dto)
            => _categoryOperations
                .Update(id, dto)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromEntity(c)));


        [HttpGet("{id}",Name = "GetCategory")]
        public IActionResult GetCategory(long id)
            => _categoryOperations
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(FromEntity(c)));

        [HttpGet]
        public ActionResult GetCategories()
        {
            var result = _categoryOperations.GetAll();
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) 
            => _categoryOperations.Delete(id)
            .Match<ActionResult>(
                Left: err => BadRequest(err.Message),
                Right: c => NoContent());
    }
}