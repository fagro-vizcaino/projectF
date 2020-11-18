using ProjectF.Api.Features.Product;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Warehouses;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectF.Api.Features.Werehouses
{
    public class WarehouseViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public WarehouseDto ToDto() => new WarehouseDto(Id, Code, Name, Location);
        public static WarehouseViewModel FromDto(WarehouseDto werehouse)
            => new WarehouseViewModel()
            {
                Id = werehouse.Id,
                Code = werehouse.Code,
                Name = werehouse.Name,
                Location = werehouse.Location
            };

    }
}
