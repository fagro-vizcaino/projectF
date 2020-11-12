using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceDetail : FEntity
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxPercent { get; set; }
    }
}
