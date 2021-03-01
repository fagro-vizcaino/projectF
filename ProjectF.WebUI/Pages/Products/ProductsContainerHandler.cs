using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.Categories;
using ProjectF.WebUI.Pages.Taxes;
using ProjectF.WebUI.Pages.UnitOfMeasures;
using ProjectF.WebUI.Pages.Werehouses;
using ProjectF.WebUI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Products
{
    public class ProductContainerHandler : BaseContainerBasicCrud<Product>
    {
        public ProductContainerHandler() : base("Productos")
        {
            var emptyModel = new Product
            {
                Id = 0,
                Code = string.Empty,
                Name = string.Empty,
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        [Inject] private IBaseDataService<Category> CategoryDataService { get; set; }
        protected Category[] Categories { get; set; } = Array.Empty<Category>();

        [Inject] private IBaseDataService<Warehouse> WarehouseDataService { get; set; }
        protected Warehouse[] Warehouses { get; set; } = Array.Empty<Warehouse>();

        [Inject] private IBaseDataService<Tax> TaxDataService { get; set; }
        protected Tax[] Taxes { get; set; } = Array.Empty<Tax>();

        [Inject]
        public IBaseDataService<UnitOfMeasure> UnitOfMeasureDataService { get; set; }
        public UnitOfMeasure[] UnitOfMeasures { get; set; } = Array.Empty<UnitOfMeasure>();
        public ProductListParameters QueryParameters { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            QueryParameters = new ProductListParameters();
            Elements       = (await DataService.Get(QueryParameters)).ToArray();
            Categories     = (await CategoryDataService.GetAll()).ToArray();
            Warehouses     = (await WarehouseDataService.GetAll()).ToArray();
            Taxes          = (await TaxDataService.GetAll()).ToArray();
            UnitOfMeasures = (await UnitOfMeasureDataService.GetAll()).ToArray();
        }

        protected Product GetNewModelOrEdit(Product product = null)
            => product != null
            ? new Product
            {
                Id              = product.Id,
                Name            = product.Name,
                Category        = product.Category ?? new Category { Id = product.CategoryId },
                CategoryId      = product.CategoryId,
                UnitOfMeasure   = product.UnitOfMeasure ?? new UnitOfMeasure { Id = product.UnitOfMeasureId },
                UnitOfMeasureId = product.UnitOfMeasureId,
                TaxId           = product.TaxId,
                Tax             = product.Tax ?? new Tax { Id = product.TaxId },
                Code            = product.Code,
                Cost            = product.Cost,
                Description     = product.Description,
                IsService       = product.IsService,
                Price           = product.Price,
                Price2          = product.Price2,
                Price3          = product.Price3,
                Price4          = product.Price4,
                Reference       = product.Reference,
                Warehouse       = product.Warehouse ?? new Warehouse { Id = product.WarehouseId },
                WarehouseId     = product.WarehouseId
            }
            : new Product { Code = GenerateCode};
    }
}
