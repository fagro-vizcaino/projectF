using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices.InvoiceList
{
    public class InvoiceListHandler : ComponentBase
    {
        public bool DropDownDateOpen { get; set; } = false;
        public bool DropDownOpen { get; set; } = false;
        public bool DropDownStatusOpen { get; set; } = false;

        [Inject]
        public IBaseDataService<Invoice> Invoices { get; set; }
        
    }
}
