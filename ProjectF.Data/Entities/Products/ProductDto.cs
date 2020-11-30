using System;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Data.Entities.Products
{
    public record ProductDto(long Id
        , string Code
        , string Name
        , string Description
        , string Reference
        , Category Category
        , long CategoryId
        , Warehouse Warehouse
        , long WarehouseId
        , long TaxId
        , Tax Tax
        , bool IsService
        , decimal Cost
        , decimal Price
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}