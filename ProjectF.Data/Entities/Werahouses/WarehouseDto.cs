using ProjectF.Data.Entities.Common;
using System.Collections.Immutable;
using ProjectF.Data.Products;
using System;

namespace ProjectF.Data.Entities.Warehouses
{
    public record WarehouseDto(long Id
        , string Code
        , string Name
        , string Location
        , ImmutableList<Product> Products
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
