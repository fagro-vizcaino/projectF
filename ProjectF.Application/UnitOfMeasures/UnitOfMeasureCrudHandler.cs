using LanguageExt;
using LanguageExt.Common;
using ProjectF.Application.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.UnitOfMeasures;
using ProjectF.Data.Repositories;
using static ProjectF.Application.UnitOfMeasures.UnitOfMeasureMapper;

namespace ProjectF.Application.UnitOfMeasures
{
    public class UnitOfMeasureCrudHandler : BaseCrudHandler<UnitOfMeasureDto, UnitOfMeasure, UnitOfMeasureRepository>
    {
        public UnitOfMeasureCrudHandler(UnitOfMeasureRepository unitOfMeasureRepository) : base(unitOfMeasureRepository)
            => (_, _fromDto, _fromEntity, _updateEntity  ) = (unitOfMeasureRepository, FromDto, FromEntity, UpdateEntity);

        Either<Error, UnitOfMeasure> UpdateEntity(UnitOfMeasureDto dto, UnitOfMeasure unitOfMeasure)
        {
           unitOfMeasure.EditUnitOfMeasure(new Name(dto.Name), dto.Value, dto.Status);
           return unitOfMeasure;
        }
    }
}
