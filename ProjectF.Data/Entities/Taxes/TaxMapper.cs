using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Taxes
{
    public static class TaxMapper
    {
        public static Tax FromDto(TaxDto dto)
            => new Tax(new Name(dto.Name)
                , dto.PercentValue
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static TaxDto FromEntity(Tax entity)
            => new TaxDto(entity.Id
                , entity.Name.Value
                , entity.PercentValue
                , entity.Created
                , entity.Modified
                , entity.Status);
    }
}
