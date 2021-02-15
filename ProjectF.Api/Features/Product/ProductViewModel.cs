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
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ProjectF.Api.Features.Product
{
  public class ProductViewModel
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Reference { get; set; }
    public CategoryDto Category { get; set; }
    public WarehouseDto Warehouse { get; set; }
    public TaxViewModel Tax { get; set; }
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
      var category = new CategoryDto(productDto.Category.Code.Value
      , productDto.Category.Name.Value
      , productDto.Category.ShowOn
      , productDto.Category.Created
      , productDto.Modified) with
      { Status = productDto.Category.Status };

      var warehouse = new WarehouseDto(productDto.Warehouse.Code.Value
      , productDto.Warehouse.Name.Value
      , productDto.Warehouse.Location
      , productDto.Warehouse.Products.ToImmutableList()) with
      {
        Status = productDto.Warehouse.Status,
        Modified = productDto.Warehouse.Modified,
        Created = productDto.Warehouse.Created
      };

      var tax = new TaxViewModel()
      {
        Id = productDto.Tax.Id,
        Name = productDto.Tax.Name.Value,
        PercentValue = productDto.Tax.PercentValue
      };

      return new ProductViewModel()
      {
        Id = productDto.Id,
        Code = productDto.Code,
        Name = productDto.Name,
        Description = productDto.Description,
        Reference = productDto.Reference,
        Category = category,
        Warehouse = warehouse,
        Tax = tax,
        IsService = productDto.IsService,
        Cost = productDto.Cost,
        Price = productDto.Price,
        Price2 = 0,
        Price3 = 0,
        Price4 = 0
      };
    }

  }
}
