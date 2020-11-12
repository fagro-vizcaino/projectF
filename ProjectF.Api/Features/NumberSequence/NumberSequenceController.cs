using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.NumberSequence;
using ProjectF.Data.Entities.Sequences;
using System.Linq;

namespace ProjectF.Api.Features.NumberSequence
{
    [Route("api/settings/[controller]")]
    [ApiController]
    public class NumberSequenceController: ControllerBase
    {
        private readonly DocumentNumberSequenceHandler _sequenceHandler;

        public NumberSequenceController(DocumentNumberSequenceHandler sequenceHandler)
        {
            this._sequenceHandler = sequenceHandler;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result =  _sequenceHandler.GetAll()
               .Select(c => c);
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateNumberSequence(NumberSequenceDto sequenceDto)
            => _sequenceHandler
                .Create(sequenceDto)
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(c));


        [HttpPut("{id}")]
        public ActionResult UpdateNumberSequence(long id, NumberSequenceDto sequenceDto)
            => _sequenceHandler
                .Update(id, sequenceDto)
                .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(sequenceDto));


        [HttpGet("{id}")]
        public ActionResult GetNumberSequence(long id)
            => _sequenceHandler.Find(id).Match<ActionResult>(
                    Left: err => BadRequest(err),
                    Right: c => Ok(c));

        
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
            => _sequenceHandler.Delete(id).Match<ActionResult>(Left: err => BadRequest(err.Message),
                Right: c => Ok(c));

    }
}
