using ProjectF.Data.Entities.Warehouses;
using ProjectF.Application.Common;
using static ProjectF.Application.Warehouses.WarehouseMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;

namespace ProjectF.Application.Warehouses
{
    public class WarehouseCrudHandler : BaseCrudHandler<WarehouseDto, Warehouse, WerehouseRepository>
    {
        public WarehouseCrudHandler(WerehouseRepository warehouseRepo) : base(warehouseRepo)
            => (_, _fromDto, _fromEntity, _updateEntity)
            = (warehouseRepo, FromDto, FromEntity, UpdateEntity);

        Either<Error, Warehouse> UpdateEntity(WarehouseDto dto, Warehouse warehouse)
        {
            var code = new Code(dto.Code);
            var name = new Name(dto.Name);
            warehouse.EditWerehouse(code, name, dto.Location, dto.Status);
            return warehouse;
        }
    }
}