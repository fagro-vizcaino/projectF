using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.Application.Invoice;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.Api.Features.Invoice
{
    [Route("api/sales/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        readonly InvoiceCrudHandler _invoiceOperation;

        public InvoiceController(InvoiceCrudHandler invoiceOperation)
        {
            _invoiceOperation = invoiceOperation;
        }

         [HttpPost]
        public ActionResult CreateInvoice(InvoiceViewModel viewModel)
            => _invoiceOperation
                .Create(viewModel.ToDto())
              .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: invoice => Ok(InvoiceViewModel.FromDto(_invoiceOperation.EntityToDto(invoice)))
                    );

        [HttpPut("{id}")]
        public ActionResult UpdateInvoice(long id, InvoiceViewModel viewModel)
            => _invoiceOperation
                .Update(id, viewModel.ToDto())
                 .Match<ActionResult>(
                    Left: err => BadRequest(err.Message),
                    Right: c => Ok(InvoiceViewModel.FromDto(_invoiceOperation.EntityToDto(c))));

        [HttpGet("{id}")]
        public IActionResult GetInvoice(long id)
            => _invoiceOperation
                .Find(id)
                .Match<ActionResult>(
                    Left: err => NotFound(err.Message),
                    Right: c => Ok(InvoiceViewModel.FromDto(_invoiceOperation.EntityToDto(c))));

        [HttpGet]
        public ActionResult GetInvoice()
        {
            var result = _invoiceOperation.GetAll()
                .Select(c => InvoiceViewModel.FromDto(c));
            if (result.Any()) return Ok(result);

            return NotFound();
        }
    }
}
