using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.PaymentMethods;
using ProjectF.Data.Entities.PaymentMethods;
using static ProjectF.Data.Entities.PaymentMethods.PaymentMethodMapper;

namespace ProjectF.Api.Features.PaymentMethod
{
    [Route("api/config/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        readonly PaymentMethodHandler _paymentMethodHandler;

        public PaymentMethodController(PaymentMethodHandler paymentMethodHandler)
            => _paymentMethodHandler = paymentMethodHandler;

        [HttpPost]
        public ActionResult CreatePaymentTerm(PaymentMethodDto dto)
            => _paymentMethodHandler.Create(dto)
            .Match<ActionResult>(Left: err => BadRequest(err.Message),
                Right: m => CreatedAtRoute(nameof(GetPaymentMethod), new { id = m.Id }, m));

        [HttpGet("{id}", Name ="GetPaymentMethod")]
        public ActionResult GetPaymentMethod(long id)
            => _paymentMethodHandler.Find(id)
            .Match<ActionResult>(Left: err => NotFound(err.Message),
                Right: m => Ok(FromEntity(m)));

        [HttpPut("{id}")]
        public ActionResult UpdatePaymentMethod(long id, PaymentMethodDto dto)
            => _paymentMethodHandler.Update(id, dto)
            .Match<ActionResult>(Left: err => BadRequest(err.Message),
                Right: m => CreatedAtRoute(nameof(GetPaymentMethod), new { id = m.Id}, dto));

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
            => _paymentMethodHandler.Delete(id)
            .Match<ActionResult>(Left: err => BadRequest(err.Message),
                Right: _ => NoContent());

        [HttpGet]
        public ActionResult GetPaymentMethod()
        {
            var result = _paymentMethodHandler.GetAll();
            if(result.Any()) return Ok(result);
            return NotFound();
        }
            
    }
}
