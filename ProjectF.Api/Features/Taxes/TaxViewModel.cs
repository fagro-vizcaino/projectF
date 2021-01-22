using System;
using ProjectF.Application.Common;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Api.Features.Taxes
{
    public class TaxViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PercentValue { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }
        public TaxDto ToDto()
            => (new TaxDto(Name, PercentValue, CompanyId, Created, Modified)) 
                with { Id = Id, Status = Status };

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
