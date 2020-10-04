using ProjectF.Api.Features.Product;
using ProjectF.Api.Features.Tax;
using System.Security.Principal;

namespace ProjectF.Api.Features.Invoice
{
    public class InvoiceDetailViewModel
    {
        public long Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxPercent  { get; set; }
    }
}
