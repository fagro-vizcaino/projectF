using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;

namespace ProjectF.WebUI.Pages.Invoices
{
    public class InvoiceContainerHandler : BaseContainerBasicCrud<Invoice>
    {
        public InvoiceContainerHandler() : base("Factura")
        {
            var emtpyModel = new Invoice
            {
                Id = 0,
                Code = string.Empty,
                Client = new Client()
            };
            InitModel(emtpyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        [Inject]
        public IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = Array.Empty<PaymentTerm>();

        public Invoice GetNewModelOrEdit(Invoice tax = null)
            => tax != null
            ? new Invoice
            {
                Id = tax.Id,
                Code = tax.Code,
            }
            : new Invoice { Id = 0, Code = GenerateCode };

    }
}
