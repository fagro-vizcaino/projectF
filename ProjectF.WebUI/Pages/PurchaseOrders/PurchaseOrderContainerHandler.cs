using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;
using ProjectF.WebUI.Services;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrderContainerHandler : BaseContainerBasicCrud<PurchaseOrder>
    {
        public ButtonTypes ButtonTypes { get; set; }
        public PurchaseOrderDetail Line { get; set; }
        public Product SelectedProduct { get; set; }
        public PurchaseOrderContainerHandler() : base("Order de Compra")
        {
            var emptyPurchase = new PurchaseOrder() { Id = 0, Created = DateTime.UtcNow, DeliverDate = DateTime.UtcNow };
            InitModel(emptyPurchase);
            NewOrEditOperation = GetNewModelOrEdit;
            Line = new PurchaseOrderDetail();
            SelectedProduct = new Product();
        }

        [Inject]
        public IBaseDataService<Supplier> SupplierDataService { get; set; }
        public Supplier[] Suppliers { get; set; } = System.Array.Empty<Supplier>();

        [Inject]
        public IBaseDataService<Product> ProductDataService { get; set; }
        public Product[] Products { get; set; } = System.Array.Empty<Product>();

        [Inject]
        public IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = System.Array.Empty<PaymentTerm>();

        [Inject]
        public IBaseDataService<Warehouse> WarehouseDataService { get; set; }
        public Warehouse[] Warehouses { get; set; } = System.Array.Empty<Warehouse>();


        [Inject]
        public IBaseDataService<Tax> TaxDataService { get; set; }
        public Tax[] Taxes { get; set; } = System.Array.Empty<Tax>();

        public List<PurchaseOrderDetail> Details { get; set; } = new List<PurchaseOrderDetail>
        {
            new () { Cost = 20.0m, Description = "Batida1", DiscountValue = 0, ProductCode = "sd", Id = 1, Qty =2, TaxPercent = 0 },
            new () { Cost = 19.9m, Description = "Yacuta", DiscountValue = 0, ProductCode = "sd", Id = 1, Qty =1, TaxPercent = 0 },
            new () { Cost = 55.50m, Description = "Bastovia", DiscountValue = 0, ProductCode = "sd", Id = 1, Qty =3, TaxPercent = 0 },
        };
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();
            Suppliers = (await SupplierDataService.GetAll()).ToArray();
            Warehouses = (await WarehouseDataService.GetAll()).ToArray();
            PaymentTerms = (await PaymentTermDataService.GetAll()).ToArray();
            Products = (await ProductDataService.GetAll()).ToArray();
            Taxes = (await TaxDataService.GetAll()).ToArray();
        }
        protected void OnSelectProductChangedHandler(Product item)
        {
            Line.ProductCode = item.Code;
            Line.Description = item.Name;
            Line.Qty = 1;
            Line.Cost = item.Cost;

        }

        protected void OnSelectSupplierChangedHandler(int supplierId)
        {
            var supplier = Suppliers.Find( c => c.Id == supplierId)
                .Match(c => MapSupplierData(c), () => ClearMapSupplierData());
        }

        void MapSupplierData(Supplier supplier)
        {
            _model.Rnc           = supplier.Rnc;
            _model.SupplierId    = supplier.Id;
            _model.SupplierName  = supplier.Name;
            _model.PaymentTermId = supplier.PaymentTermId;
            _model.Address       = $"{supplier.Street} {supplier.HomeOrApartment} {supplier.City}";
        }

        void ClearMapSupplierData()
            => _model = GetNewModelOrEdit();

        public PurchaseOrder GetNewModelOrEdit(PurchaseOrder purchaseOrder = null)
        {
            Console.WriteLine($"new or edit");
            return new PurchaseOrder() { Id = 0, Created = DateTime.UtcNow, DeliverDate = DateTime.UtcNow };
        }
    }
}
