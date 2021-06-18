using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Application.Taxes
{
    public static class TaxMapper
    {
        public static Tax FromDto(TaxDto dto)
            => new(new Name(dto.Name)
                , dto.PercentValue
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static TaxDto FromEntity(Tax entity)
            => new(entity.Name.Value
                , entity.PercentValue)
            {
                Id = entity.Id, 
                Status = entity.Status, 
                Created = entity.Created,
                Modified = entity.Modified
            };
    }
}
