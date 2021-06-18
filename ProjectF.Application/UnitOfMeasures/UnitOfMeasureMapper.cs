
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.UnitOfMeasures;

namespace ProjectF.Application.UnitOfMeasures
{
    public static class UnitOfMeasureMapper
    {
        public static UnitOfMeasure FromDto(UnitOfMeasureDto dto)
            => new (new Name(dto.Name)
                , dto.Value
                , dto.Created
                , dto.Status);

        public static UnitOfMeasureDto FromEntity(UnitOfMeasure entity)
            => new(entity.Name.Value
                , entity.Value)
            {
                Id = entity.Id,
                Status = entity.Status, 
                Created = entity.Created, 
                Modified = entity.Modified
            };
    }
}
