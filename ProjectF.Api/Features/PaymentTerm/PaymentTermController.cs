using ProjectF.Application.PaymentTerms;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ProjectF.Data.Entities.PaymentList;
using static ProjectF.Data.Entities.PaymentList.PaymentTermMapper;

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
        public ActionResult CreatePaymentTerm(PaymentTermDto dto)
            => _paymentTermOperation
                .Create(dto)
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetPaymentTerm), 
                        new { id = dto.Id }, dto));


        [HttpPut("{id}")]
        public ActionResult UpdatePaymentTerm(long id, PaymentTermDto dto)
            => _paymentTermOperation
                .Update(id, dto)
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: pt => CreatedAtRoute(nameof(GetPaymentTerm), 
                        new { id = pt.Id }, dto));


        [HttpGet("{id}", Name = "GetPaymentTerm")]
        public IActionResult GetPaymentTerm(long id)
            => _paymentTermOperation
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(FromEntity(c)));

        [HttpGet]
        public ActionResult GetPaymentTerms()
        {
            var result = _paymentTermOperation.GetAll()
                .Select(c => c);
            if (result.Any()) return Ok(result);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
           => _paymentTermOperation.Delete(id)
           .Match<ActionResult>(
               Left: err => BadRequest(err.Message),
               Right: _ => NoContent());

        //[HttpGet]
        //public ActionResult GetAll([FromQuery] PaginationQuery paginationQuery )
        //{
        //    var result = _categoryOperations.GetAll()
        //    return Ok();
        //}
    }
}
