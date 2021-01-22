using ProjectF.Api.Features.Categories;
using ProjectF.Api.Features.Taxes;
using ProjectF.Api.Features.Werehouses;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Warehouses;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Taxes;
using System;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Api.Features.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public CategoryViewModel Category { get; set; }
        public WarehouseViewModel Warehouse { get; set; }
        public TaxViewModel Tax { get; set;}
        public bool IsService { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }
        public ProductDto ToDto()
        {
            var category = new Category(new Code(Category.Code),
                new Name(Category.Name),
                Category.ShowOn,
                Category.Created,
                Category.Status);

            var warehouse = new Warehouse(new Code(Warehouse.Code)
                , new Name(Warehouse.Name)
                , Warehouse.Location
                , Warehouse.Created
                , Warehouse.Status);

            var tax = new Tax(new Name(Tax.Name)
                , Tax.PercentValue
                , Tax.Created
                , Tax.Modified
                , Tax.CompanyId
                , Tax.Status);

            return new ProductDto(Id
               , Code
               , Name
               , Description
               , Reference
               , category
               , Category.Id
               , warehouse
               , Warehouse.Id
               , Tax.Id
               , tax
               , IsService
               , Cost
               , Price
               , Created
               , Modified
               , Status);
        }

        public static ProductViewModel FromDtoToView(ProductDto productDto)
        {
            var category = new CategoryViewModel()
            {
                Code   = productDto.Category.Code.Value,
                Name   = productDto.Category.Name.Value,
                Id     = productDto.Category.Id,
                ShowOn = productDto.Category.ShowOn
            };

            var werehouse = new WarehouseViewModel()
            {
                Code     = productDto.Warehouse.Code.Value,
                Name     = productDto.Warehouse.Name.Value,
                Id       = productDto.Warehouse.Id,
                Location = productDto.Warehouse.Location,
            };

            var tax = new TaxViewModel()
            {
                Id           = productDto.Tax.Id,
                Name         = productDto.Tax.Name.Value,
                PercentValue = productDto.Tax.PercentValue
            };

            return new ProductViewModel()
            {
                Id          = productDto.Id,
                Code        = productDto.Code,
                Name        = productDto.Name,
                Description = productDto.Description,
                Reference   = productDto.Reference,
                Category    = category,
                Warehouse   = werehouse,
                Tax         = tax,
                IsService   = productDto.IsService,
                Cost        = productDto.Cost,
                Price       = productDto.Price,
                Price2      = 0,
                Price3      = 0,
                Price4      = 0
            };
        }

    }
}
