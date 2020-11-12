using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
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
            var emtpyModel = new Product
            {
                Id = 0,
                Code = string.Empty,
                Name = string.Empty,
            };
            InitModel(emtpyModel);
            NewOrEditOperation = GetNewModelOrEdit;

        }

        [Inject]
        public IBaseDataService<Category> CategoryDataService { get; set; }
        public Category[] Categories { get; set; } = Array.Empty<Category>();

        [Inject]
        public IBaseDataService<Werehouse> WerehouseDataService { get; set; }
        public Werehouse[] Werehouses { get; set; } = Array.Empty<Werehouse>();

        [Inject]
        public IBaseDataService<Tax> TaxDataService { get; set; }
        public Tax[] Taxes { get; set; } = Array.Empty<Tax>();

        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();
            Categories = (await CategoryDataService.GetAll()).ToArray();
            Werehouses = (await WerehouseDataService.GetAll()).ToArray();
            Taxes= (await TaxDataService.GetAll()).ToArray();
        }

        public Product GetNewModelOrEdit(Product product = null)
            => product != null
            ? new Product
            {
                Id = product.Id,
                Name = product.Name,
                Category = new Category { Id = product.CategoryId },
                Code = product.Code,
                Cost = product.Cost,
                Description = product.Description,
                IsService = product.IsService,
                Price = product.Price,
                Price2 = product.Price2,
                Price3 = product.Price3,
                Price4 = product.Price4,
                Reference = product.Reference,
                Werehouse = new Werehouse { Id = product.WerehouseId }
            }
            : new Product { Code = GenerateCode };
    }
}
