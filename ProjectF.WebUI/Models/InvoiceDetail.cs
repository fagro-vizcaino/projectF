using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
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
