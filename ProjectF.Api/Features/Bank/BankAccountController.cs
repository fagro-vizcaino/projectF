
using System.Linq;
using static ProjectF.Api.Features.Bank.BankAccountViewModel;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.Banks;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Banks;
using System.Text.Json;

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
        public async Task<ActionResult> GetBankAccount([FromQuery] BankListParameters bankListParameters)
            => (await _bankAccountCrudHandler.GetBankAccountList(bankListParameters))
             .Match<ActionResult>(Left: err => NotFound(err.Message), Right: c => {
                 Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(c.meta));
                 return Ok(c.list);
             });

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
         => _bankAccountCrudHandler.Delete(id)
         .Match<ActionResult>(
             Left: err => BadRequest(err.Message),
             Right: c => NoContent());
    }
}
