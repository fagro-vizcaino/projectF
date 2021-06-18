using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Core;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.UnitOfMeasures;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Data.Entities.Products
{
    public record ProductDto(string Code
        , string Name
        , string? Description
        , string? Reference
        , CategoryDto Category
        , WarehouseDto Warehouse
        , UnitOfMeasureDto UnitOfMeasure
        , TaxDto Tax
        , bool IsService
        , decimal Cost
        , decimal Price) : FDto;
}