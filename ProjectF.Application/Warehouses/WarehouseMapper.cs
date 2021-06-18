using System.Linq;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Application.Warehouses
{
    public static class WarehouseMapper
    {
        public static Warehouse FromDto(WarehouseDto dto)
            => new(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.Location
                , dto.Created
                , dto.Status);

        public static WarehouseDto FromEntity(Warehouse entity)
            => new(entity.Code.Value
                , entity.Name.Value
                , entity.Location) 
                { 
                    Id = entity.Id,
                    Products = entity.Products.ToList(),
                    Modified = entity.Modified, 
                    Created = entity.Created, 
                    Status = entity.Status
               };
    }
}
