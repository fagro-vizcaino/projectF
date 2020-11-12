using Microsoft.AspNetCore.Mvc;
using ProjectF.Api.Features.PdfGenerator;
using System.Threading.Tasks;

namespace ProjectF.Api.Features.InvoicePrint
{
    public class InvoicePrintController : BasePdfController
    {
        public ActionResult Index()
        {
            return View("PrintInvoice");
        }

        public Task<FileContentResult> PrintInvoice()
        {
            return Pdf("PrintInvoice", null);
        }

    }
}
