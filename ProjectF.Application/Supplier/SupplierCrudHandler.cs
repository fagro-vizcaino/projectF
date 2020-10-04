using ProjectF.Data.Repositories;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Suppliers;

namespace ProjectF.Application.Supplier
{
    public class SupplierCrudHandler
    {
        readonly SupplierRepository _supplierRepository;
        readonly CountryRepository _countryRepository;

        public SupplierCrudHandler(SupplierRepository supplierRepository, CountryRepository countryRepository)
        {
            _supplierRepository = supplierRepository;
            _countryRepository = countryRepository;
        }

        public Either<Error, Data.Entities.Suppliers.Supplier> Create(SupplierDto supplierDto)
            => ValidateCode(supplierDto)
            .Bind(ValidateName)
            .Bind(ValidateEmail)
            .Bind(ValidatePhone)
            .Bind(ValidateCountry)
            .Bind(SetCountry)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);


        public Either<Error, SupplierDto> Update(long id, SupplierDto supplierDto)
            => ValidateIsCorrectUpdate(id, supplierDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(s => Find(s.Id))
            .Bind(s => UpdateEntity(supplierDto, s))
            .Bind(Update);


        public IEnumerable<SupplierDto> FindAll()
            => _supplierRepository.FindAll()
                .Map(s => new SupplierDto(s.Id
                    , s.Code.Value
                    , s.Name.Value
                    , s.Email
                    , s.Phone.Value
                    , s.Rnc
                    , s.HomeOrApartment
                    , s.City
                    , s.Street
                    , s.Country.Id
                    , s.Country
                    , s.IsIndependent));

        public Either<Error, Data.Entities.Suppliers.Supplier> Find(long key)
        {
            var supplier = _supplierRepository.GetByKeys(key);
            if (supplier == null)
                return Error.New("No records found");

            return supplier;
        }

        public SupplierDto EntityToDto(Data.Entities.Suppliers.Supplier supplier)
           => new SupplierDto(supplier.Id
                    , supplier.Code.Value
                    , supplier.Name.Value
                    , supplier.Email
                    , supplier.Phone.Value
                    , supplier.Rnc
                    , supplier.HomeOrApartment
                    , supplier.City
                    , supplier.Street
                    , supplier.Country.Id
                    , supplier.Country
                    , supplier.IsIndependent);

        //Missing Pagination

        Either<Error, SupplierDto> ValidateIsCorrectUpdate(long id, SupplierDto supplierDto)
        {
            if (id == supplierDto.Id) return supplierDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, SupplierDto> ValidateCode(SupplierDto supplierDto)
            => Code.Of(supplierDto.Code)
                .Match<Either<Error, SupplierDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => supplierDto);

        Either<Error, SupplierDto> ValidateName(SupplierDto supplierDto)
            => Name.Of(supplierDto.Name)
            .Match(Succ: c => supplierDto,
                    Fail: err => Left<Error, SupplierDto>(Error.New(string.Join(":", err))));

        Either<Error, SupplierDto> ValidateEmail(SupplierDto supplierDto)
            => Email.Of(supplierDto.Email)
                .Match<Either<Error, SupplierDto>>(
                    Left: err => Error.New(err.Message),
                    Right: s => supplierDto);

        Either<Error, SupplierDto> ValidatePhone(SupplierDto supplierDto)
            => Phone.Of(supplierDto.Phone)
                .Match<Either<Error, SupplierDto>>(
                    Left: err => Error.New(err.Message),
                    Right: s => supplierDto);

        Either<Error, SupplierDto> ValidateCountry(SupplierDto supplier)
        {
            if (supplier.Country == null && supplier.SelectedCountry == 0)
                return Error.New("country is required");

            return supplier;
        }

        Either<Error, SupplierDto> SetCountry(SupplierDto supplier)
        {
            var country = _countryRepository.FromCountryId(supplier.SelectedCountry);
            if (country == null) return Error.New("couldn't find to country");

            var nSupplier = supplier.With(country:country);
            return nSupplier;
        }

        Either<Error, Data.Entities.Suppliers.Supplier> CreateEntity(SupplierDto supplierDto)
        {
            var code = new Code(supplierDto.Code);
            var name = new Name(supplierDto.Name);
            var email = new Email(supplierDto.Email);
            var phone = new Phone(supplierDto.Phone);

            var supplier = new Data.Entities.Suppliers.Supplier(code
                , name
                , email
                , phone
                , supplierDto.Rnc
                , supplierDto.HomeOrApartment
                , supplierDto.City
                , supplierDto.Street
                , supplierDto.Country
                , supplierDto.IsIndependent);

            return supplier;
        }

        Either<Error, Data.Entities.Suppliers.Supplier> UpdateEntity(SupplierDto supplierDto, Data.Entities.Suppliers.Supplier supplier)
        {
            var code = new Code(supplierDto.Code);
            var name = new Name(supplierDto.Name);
            var email = new Email(supplierDto.Email);
            var phone = new Phone(supplierDto.Phone);

            supplier.EditSupplier(code
                , name
                , email
                , phone
                , supplierDto.Rnc
                , supplierDto.HomeOrApartment
                , supplierDto.City
                , supplierDto.Street
                , supplierDto.Country
                , supplierDto.IsIndependent);

            return supplier;
        }

        Either<Error, Data.Entities.Suppliers.Supplier> Add(Data.Entities.Suppliers.Supplier supplier)
        {
            try
            {
                _supplierRepository.Add(supplier);
                return supplier;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Data.Entities.Suppliers.Supplier> Save(Data.Entities.Suppliers.Supplier supplier)
        {
            try
            {
                _supplierRepository.Save();
                return supplier;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, SupplierDto> Update(Data.Entities.Suppliers.Supplier supplier)
        {
            try
            {
                _supplierRepository.Save();
                return EntityToDto(supplier);
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
