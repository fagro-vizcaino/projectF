using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public static class UnitOfMeasureMapper
    {
        public static UnitOfMeasure FromDto(UnitOfMeasureDto dto)
            => new UnitOfMeasure(new Name(dto.Name)
                , dto.Created
                , dto.Status);

        public static UnitOfMeasureDto FromEntity(UnitOfMeasure entity)
            => new UnitOfMeasureDto(entity.Id
                , entity.Name.Value
                , entity.Created
                , entity.Modified
                , entity.Status);
    }
}
