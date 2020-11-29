using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Api.Features.Taxes
{
    public class TaxViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal PercentValue { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }
        public TaxDto ToDto() => new TaxDto(Id, Name, PercentValue, Created, Modified, Status);

        public static TaxViewModel FromDtoToView(TaxDto tax)
            => new TaxViewModel()
            {
                Id           = tax.Id,
                Name         = tax.Name,
                PercentValue = tax.PercentValue,
                Created      = tax.Created,
                Modified     = tax.Modified,
                Status       = tax.Status
            };
    }
}
