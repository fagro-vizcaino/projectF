using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Api.Features.Tax
{
    public class TaxViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal PercentValue { get; set; }

        public TaxDto ToDto() => new TaxDto(Id, Name, PercentValue);

        public static TaxViewModel FromDto(TaxDto tax)
            => new TaxViewModel()
            {
                Id           = tax.Id,
                Name         = tax.Name,
                PercentValue = tax.PercentValue
            };
    }
}
