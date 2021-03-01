using ProjectF.Data.Entities.Taxes;
using static ProjectF.Application.Taxes.TaxMapper;
using ProjectF.Data.Repositories;
using ProjectF.Application.Common;
using LanguageExt.Common;
using LanguageExt;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Taxes
{
    public class TaxCrudHandler : BaseCrudHandler<TaxDto, Tax, TaxRepository>
    {
        public TaxCrudHandler(TaxRepository taxRepository) : base(taxRepository)
            => (_, _fromDto, _fromEntity, _updateEntity) 
            = (taxRepository, FromDto, FromEntity, UpdateEntity);

        Either<Error, Tax> UpdateEntity(TaxDto dto, Tax tax)
        {
            var name = new Name(dto.Name);
            tax.EditTax(name, tax.PercentValue, tax.Status);
            return tax;
        }
    }
}
