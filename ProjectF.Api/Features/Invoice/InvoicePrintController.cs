using Microsoft.AspNetCore.Mvc;
using ProjectF.Api.Features.PdfGenerator;
using ProjectF.Api.Features.PdfGenerator.AspNetPrintPdf.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Api.Features.Invoice
{
    public class InvoicePrintController : BasePdfController
    {

        public Task<FileContentResult> PreviewInvoice(InvoiceViewModel viewModel)
        {
            return Pdf("InvoicePreview", viewModel);
        }

        public Task<FileContentResult> PrintInvoice(int invoiceId)
        {
            return Pdf("UserDocument", new { });
        }
    }
}
