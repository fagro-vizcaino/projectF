using ProjectF.Data.Entities.Common;
using System.Collections.Immutable;
using ProjectF.Data.Products;
using System;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.Warehouses
{
    public record WarehouseDto(string Code
        , string Name
        , string Location
        , ImmutableList<Product>? Products): FDto;
}
