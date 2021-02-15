using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Data.Entities.Warehouses
{
    public static class WarehouseMapper
    {
        public static Warehouse FromDto(WarehouseDto dto)
            => new Warehouse(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.Location
                , dto.Created
                , dto.Status);

        public static WarehouseDto FromEntity(Warehouse entity)
            => new WarehouseDto(entity.Code.Value
                , entity.Name.Value
                , entity.Location
                , entity.Products.ToImmutableList()) with { Id = entity.Id 
                , Modified = entity.Modified
                , Created = entity.Created
                , Status = entity.Status };
    }
}
