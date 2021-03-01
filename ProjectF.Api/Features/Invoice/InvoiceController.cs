using System.Threading.Tasks;
using ProjectF.Application.Invoice;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Data.Entities.Invoices;
using static ProjectF.Data.Entities.Invoices.InvoiceMapper;
using System.Text.Json;

namespace ProjectF.Api.Features.Invoice
{
    [Route("api/sales/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        readonly InvoiceCrudHandler _invoiceOperation;
        readonly InvoiceMainListHandler _mainListHandler;

        public InvoiceController(InvoiceCrudHandler invoiceOperation, InvoiceMainListHandler mainListHandler)
        {
            _invoiceOperation = invoiceOperation;
            _mainListHandler = mainListHandler;
        }

        [HttpGet]
        public async Task<ActionResult> GetInvoiceList([FromQuery] InvoiceListParameters invoiceListParameters)
        => (await _mainListHandler.Handle(invoiceListParameters))
                .Match<ActionResult>(Left: err => NotFound(err.Message),
                        Right: c => {
                            var (list, meta) = c;
                            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(meta));
                            return Ok(list);
                            });


        [HttpGet("{id:int}", Name = "GetInvoiceForEdit")]
        public async Task<IActionResult> GetInvoice(int id)
            => (await _invoiceOperation.FindAsync(id))
                .Match<ActionResult>(Left: err => NotFound(err.Message),
                    Right: c => Ok(InvoiceViewModel.FromDtoToView(FromEntity(c))));

        [HttpPost]
        public ActionResult CreateInvoice(InvoiceViewModel viewModel)
            => _invoiceOperation
                .Create(viewModel.ToDto())
              .Match<ActionResult>(Left: err => BadRequest(err.Message),
                    Right: i => Ok(InvoiceViewModel.FromDtoToView(FromEntity(i))));
        
        [HttpPut("{id:int}")]
        public ActionResult UpdateInvoice(int id, InvoiceViewModel viewModel)
            => _invoiceOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(Left: err => BadRequest(err.Message),
                    Right: c => Ok(InvoiceViewModel.FromDtoToView(FromEntity(c))));
        
    }
}
