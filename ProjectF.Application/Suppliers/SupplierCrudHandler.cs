using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Suppliers;
using static ProjectF.Data.Entities.Suppliers.SupplierMapper;
using ProjectF.Application.Common;

namespace ProjectF.Application.Suppliers
{
    public class SupplierCrudHandler : BaseCrudHandler<SupplierDto, Supplier, SupplierRepository>
    {
        readonly SupplierRepository _supplierRepository;
        readonly CountryRepository _countryRepository;
        readonly PaymentTermRepository _paymentTermRepository;

        public SupplierCrudHandler(SupplierRepository supplierRepository
            , CountryRepository countryRepository
            , PaymentTermRepository paymentTermRepository) : base(supplierRepository)
        {
            _supplierRepository    = supplierRepository;
            _countryRepository     = countryRepository;
            _paymentTermRepository = paymentTermRepository;
            fromDto                = FromDto;
            fromEntity             = FromEntity;
            updateEntity           = UpdateEntity;
        }

        public override Either<Error, Supplier> Create(SupplierDto dto)
            => SetCountry(dto)
            .Bind(SetPaymentTerm)
            .Bind(base.Create);

        public override Either<Error, Supplier> Update(long id, SupplierDto dto)
            => SetCountry(dto)
            .Bind(SetPaymentTerm)
            .Bind(c => base.Update(id, c));
       
        Either<Error, SupplierDto> SetCountry(SupplierDto supplier)
        {
            var country = _countryRepository.FromCountryId(supplier.SelectedCountry);
            if (country == null) return Error.New("couldn't find to country");

            var nSupplier = supplier with { Country = country };
            return nSupplier;
        }

        Either<Error, SupplierDto> SetPaymentTerm(SupplierDto dto)
         => _paymentTermRepository.Find(dto.PaymentTermId)
            .Match(p => Right(dto with { PaymentTerm = p })
            , () => Left<Error, SupplierDto>(Error.New("couldn't find payment term selected")));

     
        Either<Error, Supplier> UpdateEntity(SupplierDto dto, Supplier supplier)
        {
            var code = new Code(dto.Code);
            var name = new Name(dto.Name);
            var email = new Email(dto.Email);
            var phone = new Phone(dto.Phone);
            
            supplier.EditSupplier(code
                , name
                , email
                , phone
                , dto.SupplierGroup
                , dto.PaymentTerm
                , new GeneralText(dto.Notes)
                , dto.IsIndependent
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , supplier.Status);

            return supplier;
        }
    }
}
