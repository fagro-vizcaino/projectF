using ProjectF.Application.PaymentTerms;
using Microsoft.AspNetCore.Mvc;
using static ProjectF.Api.Features.PaymentTerms.PaymentTermViewModel;
using System.Linq;


namespace ProjectF.Api.Features.PaymentTerms
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class PaymentTermController : ControllerBase
    {
        readonly PaymentTermCrudHandler _paymentTermOperation;

        public PaymentTermController(PaymentTermCrudHandler paymentTermOperation)
            => _paymentTermOperation = paymentTermOperation;


        [HttpPost]
        public ActionResult CreatePaymentTerm(PaymentTermViewModel viewModel)
            => _paymentTermOperation
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetPaymentTerm), 
                        new { id = FromDto(pt).Id }, FromDto(pt)));


        [HttpPut("{id}")]
        public ActionResult UpdatePaymentTerm(long id, PaymentTermViewModel viewModel)
            => _paymentTermOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetPaymentTerm), 
                        new { id = pt.Id }, FromDto(pt)));


        [HttpGet("{id}", Name = "GetPaymentTerm")]
        public IActionResult GetPaymentTerm(long id)
            => _paymentTermOperation
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(FromDto(c)));


        [HttpGet]
        public ActionResult GetPaymentTerms()
        {
            var result = _paymentTermOperation.GetAll()
                .Select(c => FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _paymentTermOperation.Delete(id)
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
