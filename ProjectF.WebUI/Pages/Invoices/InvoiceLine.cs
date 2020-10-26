using AntDesign;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceLine
    {
        public int Index { get; set; }
        public Product Product { get; set; }
        public Select CurrentSelect { get; set; }
        public int Qty { get; set; }
        public bool IsEmpty { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public string OptionValue { get; set; }

    }
}
