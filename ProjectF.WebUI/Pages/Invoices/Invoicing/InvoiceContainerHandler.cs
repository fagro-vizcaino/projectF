using AntDesign;
using LanguageExt;
using static LanguageExt.Prelude;
using static System.Text.Json.JsonSerializer;
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
using ProjectF.WebUI.Pages.Products;

using System.Collections.Immutable;
using LanguageExt.Common;

namespace ProjectF.WebUI.Pages.Invoices.Invoicing
{
    public class InvoiceContainerHandler : BaseContainerBasicCrud<Invoice>
    {

        [Parameter]
        public int Id { get; set; }
        public InvoiceContainerHandler() : base("Factura")
        {
            InitModel(GetNewModelOrEdit());
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public InvoiceLines InvoiceLinesRef { get; set; }
        public DiscountType SelectedDiscount { get; set; }
        [Inject]
        public IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = System.Array.Empty<PaymentTerm>();

        [Inject]
        public IBaseDataService<Client> ClientDataService { get; set; }
        public Client[] Clients { get; set; } = System.Array.Empty<Client>();

        [Inject]
        public IBaseDataService<Product> ProductDataService { get; set; }
        public Product[] ProductsData { get; set; } = System.Array.Empty<Product>();

        [Inject]
        public IBaseDataService<Invoice> InvoiceService { get; set; }

        public List<InvoiceLine> Lines { get; private set; }
        protected override async Task OnInitializedAsync()
        {
            PaymentTerms = (await PaymentTermDataService.GetAll()).ToArray();
            Clients = (await ClientDataService.GetAll()).ToArray();
            ProductsData = (await ProductDataService.GetAll()).ToArray();
            Lines = new List<InvoiceLine> { InvoiceLinesRef.GetEmptyLine() };
            if (Id > 0)
            {
                _model = await GetById(Id);
                Lines = _model.InvoiceDetails.Map(i => new InvoiceLine
                {
                    Id = i.Id,
                    Product = ProductsData.SingleOrDefault(p => p.Code == i.ProductCode),
                    Index = i.Id,
                    IsDelete = false,
                    IsEmpty = false,
                    OptionValue = ProductsData.SingleOrDefault(p => p.Code == i.ProductCode).Id.ToString(),
                    Qty = i.Qty,
                }).ToList();
            }
        }

        public Invoice GetNewModelOrEdit(Invoice invoice = null)
            => invoice != null
            ? new Invoice
            {
                Id = invoice.Id,
                Code = invoice.Code,
                Client = invoice.Client,
                PaymentTerm = invoice.PaymentTerm,
                PaymentTermId = invoice.PaymentTerm.Id,
                Created = DateTime.Today,
                InvoiceDetails = invoice.InvoiceDetails
            }
            : new Invoice
            {
                Id = 0,
                Code = GenerateCode,
                Client = new Client(),
                PaymentTerm = new PaymentTerm(),
                Created = DateTime.Today,
                InvoiceDetails = new List<InvoiceDetail>()
            };

        protected void OnChangeClient(OneOf<string, IEnumerable<string>,
            LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        {
            _model.Client = Clients
                .FirstOrDefault(c => c.Id == parseLong(value.Value.ToString()).Match(i => i, () => 0));
            _model.Rnc = _model.Client.Rnc;
        }

        protected void OnChangePaymentTerm(OneOf<string, IEnumerable<string>,
            LabeledValue, IEnumerable<LabeledValue>> value, OneOf<SelectOption, IEnumerable<SelectOption>> option)
        {
            _model.PaymentTerm = PaymentTerms
                .FirstOrDefault(c => c.Id == parseLong(value.Value.ToString()).Match(i => i, () => 0));
            _model.PaymentTermId = _model.PaymentTerm.Id;
        }

        protected void ClearLines() => InvoiceLinesRef.ClearLines();

        decimal ApplyDiscount(Invoice model, DiscountType selected) => selected switch
        {
            DiscountType.Value => model.Discount,
            DiscountType.Percent => (model.Discount / 100) * model.SubTotal,
            _ => 0.00m
        };

        ImmutableList<InvoiceLine> GetValidInvoiceLine(List<InvoiceLine> lines)
            => lines.Filter(l => !l.IsDelete && l.Product.Code != null).ToImmutableList();
        public void OnLineChandedHandler(List<InvoiceLine> lines)
           => Calculate(GetValidInvoiceLine(lines));

        void Calculate(ImmutableList<InvoiceLine> lines)
        {
            _model.SubTotal = lines.Sum(i => i.Qty + i.Product.Price);
            _model.TaxTotal = lines.Sum(i => (i.Qty + i.Product.Price) * i.Product.Tax.Percentvalue / 100);
            var discount = ApplyDiscount(_model, SelectedDiscount);
            _model.Total = (_model.SubTotal - discount) + _model.TaxTotal;
        }

        protected void OnDiscountInput(ChangeEventArgs e, Action<decimal> setProperty)
        {
            try { setProperty(parseDecimal(e.Value.ToString()).Match(v => v, () => 0.00m)); }
            catch (Exception ex)
            {
                setProperty(0);
            }
            Calculate(GetValidInvoiceLine(InvoiceLinesRef.InvoiceLines));
        }

        public Invoice ToDto()
        {
            var invoiceDetail = InvoiceLinesRef.InvoiceLines.Map(i => new InvoiceDetail
            {
                Id = i.Id,
                Amount = i.Product.Price,
                ProductCode = i.Product.Code,
                ProductDescription = i.Product.Name,
                Qty = i.Qty,
                TaxPercent = i.Product.Tax.Percentvalue

            }).Filter(c => c.ProductCode != null).ToList();

            _model.Rnc = _model.Client.Rnc;
            _model.Rnc = string.Join("", Guid.NewGuid().ToString().Take(11));
            _model.InvoiceDetails = invoiceDetail;

            return _model;
        }

        public async Task Save()
        {
            if (_model.Id > 0)
            {
                await UpdateInvoice();
            }
            else
            {
                await SaveInvoice();
            }
        }

        public async Task SaveInvoice()
        {
            _model = ToDto();
            _model.Ncf = $"b010000{new Random().Next(1000, 9999)}";

            Console.WriteLine($"saving.. {Serialize(_model)}");
            await DataService.Add(_model)
               .Match(async c =>
               {
                   await FMessage.Success($"Factura: {c.Id} guardada", 3);
               }, () => Error.New("Error while creating"));
        }

        public async Task UpdateInvoice()
        {
            _model = ToDto();
            Console.WriteLine($"updating.. {Serialize(_model)}");
            await DataService.Update(_model.Id, _model)
               .Match(async c =>
               {
                   await FMessage.Success($"Factura: {c.Id} guardada", 3);
               }, () => Error.New("Error while creating"));
        }

    }

    public enum DiscountType { Value, Percent }

}
