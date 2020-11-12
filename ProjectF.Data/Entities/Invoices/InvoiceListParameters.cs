using ProjectF.Data.Entities.RequestFeatures;
using System;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceListParameters : RequestParameters
    {
        public DateTime DateFrom { get; set; } = DateTime.Today.AddMonths(-2);
        public DateTime DateTo { get; set; } = DateTime.Today;
    }
}
