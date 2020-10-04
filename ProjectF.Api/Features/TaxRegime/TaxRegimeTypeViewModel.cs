using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;

namespace ProjectF.Api.Features.TaxRegime
{
    public class TaxRegimeTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TaxRegimeDto ToDto() => new TaxRegimeDto(Id, Name, Description);
        public static TaxRegimeTypeViewModel FromDto(TaxRegimeDto taxRegimeType)
            => new TaxRegimeTypeViewModel()
            {
                Id          = taxRegimeType.Id,
                Name        = taxRegimeType.Name,
                Description = taxRegimeType.Description
            };
    }
}
