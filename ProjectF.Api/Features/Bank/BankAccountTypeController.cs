using System.Linq;
using ProjectF.Application.Banks;
using static ProjectF.Api.Features.Bank.BankAccountTypeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Bank
{
    [Route("api/finance/[controller]")]
    [ApiController]
    public class BankAccountTypeController : ControllerBase
    {
        private readonly BankAccountTypeCrudHandler  _bankAccountTypeCrudHandler;

        public BankAccountTypeController(BankAccountTypeCrudHandler bankAccountTypeCrudHandler)
        {
            _bankAccountTypeCrudHandler = bankAccountTypeCrudHandler;
        }

        [HttpPost]
        public ActionResult CreateBankAccountType(BankAccountTypeViewModel viewModel)
            => _bankAccountTypeCrudHandler
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: bt => CreatedAtRoute(nameof(GetBankAccountType),
                        new { id = bt.Id }, FromDto(bt)));


        [HttpPut("{id}")]
        public ActionResult UpdateBankAccountType(long id, BankAccountTypeViewModel viewModel)
            => _bankAccountTypeCrudHandler
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromDto(c)));


        [HttpGet("{id}")]
        public IActionResult GetBankAccountType(long id)
            => _bankAccountTypeCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Succ: c => Ok(FromDto(c)),
                    Fail: err => NotFound(string.Join("; ", err)));


        [HttpGet]
        public ActionResult GetBankAccountType()
        {
            var result = _bankAccountTypeCrudHandler.GetAll()
                .Select(c => FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
         => _bankAccountTypeCrudHandler.Delete(id)
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
