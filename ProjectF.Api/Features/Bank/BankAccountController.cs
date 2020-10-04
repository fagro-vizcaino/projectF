
using System.Linq;
using static ProjectF.Api.Features.Bank.BankAccountViewModel;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.Banks;

namespace ProjectF.Api.Features.Bank
{
    [Route("api/finance/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountCrudHandler _bankAccountCrudHandler;

        public BankAccountController(BankAccountCrudHandler bankAccountCrudHandler)
        {
            _bankAccountCrudHandler = bankAccountCrudHandler;
        }

        [HttpPost]
        public ActionResult CreateBankAccount(BankAccountViewModel viewModel)
            => _bankAccountCrudHandler
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: category => Ok(category));


        [HttpPut("{id}")]
        public ActionResult UpdateBankAccount(long id, BankAccountViewModel viewModel)
            => _bankAccountCrudHandler
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetBankAccount(long id)
            => _bankAccountCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Succ: c => Ok(FromDto(c)),
                    Fail: err => NotFound(string.Join("; ", err)));


        [HttpGet]
        public ActionResult GetBankAccount()
        {
            var result = _bankAccountCrudHandler.GetAll()
                .Select(c => FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
         => _bankAccountCrudHandler.Delete(id)
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
