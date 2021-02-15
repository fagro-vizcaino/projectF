using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Warehouses;
using ProjectF.Application.Common;
using static ProjectF.Data.Entities.Warehouses.WarehouseMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;

namespace ProjectF.Application.Werehouses
{
  public class WarehouseCrudHandler : BaseCrudHandler<WarehouseDto, Warehouse, WerehouseRepository>
  {
    readonly WerehouseRepository _werehouseRepository;

    public WarehouseCrudHandler(WerehouseRepository werehouseRepo) : base(werehouseRepo)
        => (_werehouseRepository, fromDto, fromEntity, updateEntity)
        = (werehouseRepo, FromDto, FromEntity, UpdateEntity);

    Either<Error, Warehouse> UpdateEntity(WarehouseDto dto, Warehouse werehouse)
    {
      var code = new Code(dto.Code);
      var name = new Name(dto.Name);
      werehouse.EditWerehouse(code, name, dto.Location, dto.Status);
      return werehouse;
    }
  }
}