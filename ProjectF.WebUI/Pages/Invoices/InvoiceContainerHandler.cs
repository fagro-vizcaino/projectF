using AntDesign;
using LanguageExt;
using static LanguageExt.Prelude;
using Microsoft.AspNetCore.Components;
using OneOf;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
                Client = new Client(),
                Created = DateTime.Today
            };
            InitModel(emtpyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public InvoiceLines Lines { get; set; }
        [Inject]
        public IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = System.Array.Empty<PaymentTerm>();

        [Inject]
        public IBaseDataService<Client> ClientDataService { get; set; }
        public Client[] Clients { get; set; } = System.Array.Empty<Client>();

        [Inject]
        public IBaseDataService<Product> ProductDataService { get; set; }
        public Product[] Products { get; set; } = System.Array.Empty<Product>();

        protected override async Task OnInitializedAsync()
        {
            PaymentTerms = (await PaymentTermDataService.GetAll()).ToArray();
            Clients = (await ClientDataService.GetAll()).ToArray();
            Products = (await ProductDataService.GetAll()).ToArray();
        }

        public Invoice GetNewModelOrEdit(Invoice tax = null)
            => tax != null
            ? new Invoice
            {
                Id = tax.Id,
                Code = tax.Code,
                Client = new Client(),
                Created = DateTime.Today
            }
            : new Invoice { Id = 0, Code = GenerateCode };


        protected void OnChangeClient(OneOf<string, IEnumerable<string>,
            LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        {
            var client = GetSelectedClient(value.Value.ToString());
            SetRncFromSelectedClient(client);
            SetEmailFromSelectedClient(client);
            StateHasChanged();
        }

        Client GetSelectedClient(string clientId)
            => Clients.FirstOrDefault(c => c.Id == parseLong(clientId).Match(i => i, () => 0));

        protected void ClearLines() => Lines.ClearLines();

        void SetRncFromSelectedClient(Client client) 
            => (_model.Rnc) = (client.Rnc);

        void SetEmailFromSelectedClient(Client client) 
            => (_model.Email) = (client.Email);



    }
}
