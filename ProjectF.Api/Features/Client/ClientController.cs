using ProjectF.Application.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
        public ActionResult CreateClient(ClientViewModel viewModel)
            => _clientCrudHandler
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: category => Ok(ClientViewModel.FromDto(category)));


        [HttpPut("{id}")]
        public ActionResult UpdateClient(long id, ClientViewModel viewModel)
            => _clientCrudHandler
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(ClientViewModel.FromDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetClient(long id)
            => _clientCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(ClientViewModel.FromDto(c)));


        [HttpGet]
        public ActionResult GetClients()
        {
            var result = _clientCrudHandler.GetAll()
                .Select(c => ClientViewModel.FromDto(c));
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
