using System.Linq;
using static ProjectF.Application.UnitOfMeasures.UnitOfMeasureMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.UnitOfMeasures;
using ProjectF.Data.Entities.UnitOfMeasures;

namespace ProjectF.Api.Features
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly UnitOfMeasureCrudHandler _unitOfMeasureCrudHandler;

        public UnitOfMeasureController(UnitOfMeasureCrudHandler unitOfMeasureCrudHandler)
            => _unitOfMeasureCrudHandler = unitOfMeasureCrudHandler;

        [HttpPost]
        public ActionResult Post(UnitOfMeasureDto dto)
            => _unitOfMeasureCrudHandler
                .Create(dto)
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: m => CreatedAtRoute(nameof(Get), new { id = m.Id }, m));


        [HttpPut("{id}")]
        public ActionResult Put(long id, UnitOfMeasureDto dto)
            => _unitOfMeasureCrudHandler
                .Update(id, dto)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: t => Ok(t));


        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
            => _unitOfMeasureCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: u => Ok(FromEntity(u)));


        [HttpGet]
        public ActionResult GetAll()
        {
            var result = _unitOfMeasureCrudHandler.GetAll();
            if (result.Any()) return Ok(result);

            return NotFound();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _unitOfMeasureCrudHandler.Delete(id)
           .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: c => NoContent());

    }
}
