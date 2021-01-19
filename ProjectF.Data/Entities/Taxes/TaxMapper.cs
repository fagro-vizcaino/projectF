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
                , dto.CompanyId
                , dto.Status);

        public static TaxDto FromEntity(Tax entity)
            => (new TaxDto(entity.Name.Value
                , entity.PercentValue
                , entity.CompanyId
                , entity.Created
                , entity.Modified)) with { Id = entity.Id, Status = entity.Status };
    }
}
