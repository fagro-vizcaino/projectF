using ProjectF.Api.Features.Product;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Products;
using ProjectF.Data.Entities.Warehouses;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ProjectF.Api.Features.Werehouses
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public EntityStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public WarehouseDto ToDto() => new WarehouseDto(Id, Code, Name, Location, null, Created, Modified, Status);
        public static WarehouseViewModel FromDtoToView(WarehouseDto warehouse)
            => new WarehouseViewModel()
            {
                Id       = warehouse.Id,
                Code     = warehouse.Code,
                Name     = warehouse.Name,
                Location = warehouse.Location,
                Created  = warehouse.Created,
                Modified = warehouse.Modified,
                Status   = warehouse.Status
            };
    }
}
