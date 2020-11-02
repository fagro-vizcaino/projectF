using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices.List
{
    public class InvoiceMainList
    {

        public long InvoiceId { get; set;}
        public string DisplayDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public string ClientName { get; set; }
        public Client Client { get; set; }
        public string Status { get; set;}
        public decimal Amount { get; set; }
        public decimal Balance { get; set;}

        public bool DropDownEditOpen { get; set; } = false;
    }
}
