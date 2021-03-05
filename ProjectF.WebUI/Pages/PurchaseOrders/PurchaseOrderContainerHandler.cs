using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Products;
using ProjectF.WebUI.Services;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Text.Json;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrderContainerHandler : BaseContainerBasicCrud<PurchaseOrder>
    {
        private ProductListParameters _productListParameters;
        public ButtonTypes ButtonTypes { get; set; }
        public PurchaseOrderDetail Line { get; set; }
        public Product SelectedProduct { get; set; }

        public PurchaseOrderContainerHandler() : base("Order de Compra")
        {
            var emptyPurchase = new PurchaseOrder() {Id = 0, Created = DateTime.UtcNow, DeliverDate = DateTime.UtcNow};
            InitModel(emptyPurchase);
            NewOrEditOperation = GetNewModelOrEdit;
            Line = new PurchaseOrderDetail();
            SelectedProduct = new Product();
        }

        [Inject] private IBaseDataService<Supplier> SupplierDataService { get; set; }
        public Supplier[] Suppliers { get; set; } = System.Array.Empty<Supplier>();

        [Inject] private IBaseDataService<Product> ProductDataService { get; set; }
        public Product[] Products { get; set; } = System.Array.Empty<Product>();

        [Inject] private IBaseDataService<PaymentTerm> PaymentTermDataService { get; set; }
        public PaymentTerm[] PaymentTerms { get; set; } = System.Array.Empty<PaymentTerm>();

        [Inject] private IBaseDataService<Warehouse> WarehouseDataService { get; set; }
        public Warehouse[] Warehouses { get; set; } = System.Array.Empty<Warehouse>();

        [Inject] private IBaseDataService<Tax> TaxDataService { get; set; }
        protected Tax[] Taxes { get; private set; } = System.Array.Empty<Tax>();

        public ImmutableList<PurchaseOrderDetail> Details { get; set; } = ImmutableList<PurchaseOrderDetail>.Empty;
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();
            Suppliers = (await SupplierDataService.GetAll()).ToArray();
            Warehouses = (await WarehouseDataService.GetAll()).ToArray();
            PaymentTerms = (await PaymentTermDataService.GetAll()).ToArray();
            _productListParameters = new ProductListParameters();
            Products = (await ProductDataService.Get(_productListParameters)).ToArray();
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
            var supplier = Suppliers.Find(c => c.Id == supplierId)
                .Match(MapSupplierData, ClearMapSupplierData);
        }

        protected void SavePurchaseOrderDetail(PurchaseOrderDetail line)
            => Details = Details.Add(line);

        protected void RemovePurchaseOrderDetail(PurchaseOrderDetail line)
            => Details = Details.Remove(line);
        
        void MapSupplierData(Supplier supplier)
        {
            _model.Rnc           = supplier.Rnc;
            _model.SupplierId    = supplier.Id;
            _model.SupplierName  = supplier.Name;
            _model.PaymentTermId = supplier.PaymentTermId;
            _model.Address       = $"{supplier.Street} {supplier.HomeOrApartment} {supplier.City}";
        }

        void ClearMapSupplierData() => _model = GetNewModelOrEdit();

        public PurchaseOrder GetNewModelOrEdit(PurchaseOrder purchaseOrder = null)
        {
            return new() { Id = 0, Created = DateTime.UtcNow, DeliverDate = DateTime.UtcNow };
        }
    }
}
