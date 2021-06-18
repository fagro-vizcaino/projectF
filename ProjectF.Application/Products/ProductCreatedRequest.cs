using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Core;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.UnitOfMeasures;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Application.Products
{
    public record CreateProductRequest(string Code
        , string Name
        , string? Description
        , string? Reference
        , int CategoryId
        , Category? Category
        , int WarehouseId
        , Warehouse? Warehouse
        , int UnitOfMeasureId
        , UnitOfMeasure? UnitOfMeasure
        , int TaxId
        , Tax? Tax
        , bool IsService
        , decimal Cost
        , decimal Price
        , decimal Price2
        , decimal Price3
        , decimal Price4) : FDto;
}
