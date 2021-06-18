using ProjectF.Data.Products;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ProjectF.Data.Entities.Core;
using ProjectF.Data.Entities.Products;

namespace ProjectF.Data.Entities.Warehouses
{
    public record WarehouseDto(string Code
        , string Name
        , string Location) : FDto
    {
        [JsonIgnore]
        public List<Product>? Products { get; init; }
    };
}
