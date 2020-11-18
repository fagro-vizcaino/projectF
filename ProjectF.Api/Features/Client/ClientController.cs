using ProjectF.Application.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProjectF.Data.Entities.Clients;
using static ProjectF.Data.Entities.Clients.ClientMapper;

namespace ProjectF.Api.Features.ContactClient
{
    [Route("api/contact/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientCrudHandler _clientCrudHandler;

        public ClientController(ClientCrudHandler clientOperation)
        {
            _clientCrudHandler = clientOperation;
        }

        [HttpPost]
        public ActionResult CreateClient(ClientDto model)
            => _clientCrudHandler
                .Create(model)
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(EntityToDto(c)));


        [HttpPut("{id}")]
        public ActionResult UpdateClient(long id, ClientDto model)
            => _clientCrudHandler
                .Update(id, model)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(EntityToDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetClient(long id)
            => _clientCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(EntityToDto(c)));


        [HttpGet]
        public ActionResult GetClients()
        {
            var result = _clientCrudHandler.GetAll();
            if (result.Any()) return Ok(result);

            return NotFound();
        }


        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
         => _clientCrudHandler.Delete(id)
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
