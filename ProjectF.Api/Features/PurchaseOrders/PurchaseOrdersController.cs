using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Application.PurchaseOrders;
using ProjectF.Data.Entities.PurchaseOrders;
using static ProjectF.Data.Entities.PurchaseOrders.PurchaseOrderMapper;

namespace ProjectF.Api.Features.PurchaseOrders
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        readonly PurchaseOrderCrudHandler _purchaseOrderOperation;

        public PurchaseOrdersController(PurchaseOrderCrudHandler purchaseOrderCrudHandler)
            => _purchaseOrderOperation = purchaseOrderCrudHandler;

        [HttpGet("{id}", Name = "GetPurchaseId")]
        public async Task<IActionResult> GetPurchaseId(int id)
            => (await _purchaseOrderOperation.FindAsync(id))
            .Match<ActionResult>(Left: err => NotFound(err.Message),
                Right: c => Ok(c));

        [HttpPost]
        public ActionResult Create(PurchaseOrderHeaderDto dto)
            => _purchaseOrderOperation.Create(dto)
            .Match<ActionResult>(Left: err => BadRequest(err.Message),
               Right: i => CreatedAtRoute(nameof(GetPurchaseId), new { id = i.Id }));


        [HttpPut("{id}")]
        public ActionResult Update(int id, PurchaseOrderHeaderDto dto)
            => _purchaseOrderOperation.Update(id, dto)
            .Match<ActionResult>(Left: err => BadRequest(err.Message),
                Right: c => Ok(FromEntity(c)));
    }
}
