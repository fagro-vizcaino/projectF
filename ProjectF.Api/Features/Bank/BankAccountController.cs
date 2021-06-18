using ProjectF.Data.Products;
using static ProjectF.Api.Features.Bank.BankAccountViewModel;
using static ProjectF.Application.Banks.BankAccountsMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.Banks;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Banks;
using ProjectF.Api.Infrastructure.Extensions;

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
                    Right: Ok);


        [HttpPut("{id:int}")]
        public ActionResult UpdateBankAccount(long id, BankAccountViewModel viewModel)
            => _bankAccountCrudHandler
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(FromEntity(c)));


        [HttpGet("{id:int}")]
        public IActionResult GetBankAccount(long id)
            => _bankAccountCrudHandler
                .Find(id)
                .Match<ActionResult>(
                    Succ: c => Ok(FromEntity(c)),
                    Fail: err => NotFound(string.Join("; ", err)));


        [HttpGet]
        public  Task<IActionResult> GetBankAccount([FromQuery] BankListParameters bankListParameters)
            =>  _bankAccountCrudHandler.GetBankAccountList(bankListParameters)
                .ToActionResult<BankAccountMainListDto, BankAccountDto>(Response);

        [HttpDelete("{id:int}")]
        public IActionResult Delete(long id)
         => _bankAccountCrudHandler.Delete(id)
         .Match<ActionResult>(
             Left: err => BadRequest(err.Message),
             Right: c => NoContent());
    }
}
