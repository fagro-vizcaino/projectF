using ProjectF.Data.Repositories;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Suppliers;
using static ProjectF.Data.Entities.Suppliers.SupplierMapper;
using System;

namespace ProjectF.Application.Suppliers
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

        public Either<Error, Supplier> Create(SupplierDto supplierDto)
            => ValidateCode(supplierDto)
            .Bind(ValidateName)
            .Bind(ValidateEmail)
            .Bind(ValidatePhone)
            .Bind(ValidateCountry)
            .Bind(SetCountry)
            .Bind(SetStatus)
            .Bind(c => Add(FromDto(c)))
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
                .Map(s => FromEntity(s));

        public Either<Error, Supplier> Find(long key)
        {
            var supplier = _supplierRepository.GetByKeys(key);
            return supplier is null
                ? Error.New("No records found")
                : (Either<Error, Supplier>)supplier;
        }

        public Either<Error, Supplier> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

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

            var nSupplier = supplier with { Country = country };
            return nSupplier;
        }

        Either<Error, SupplierDto> SetStatus(SupplierDto dto)
            => dto with { Status = Data.Entities.Common.EntityStatus.Active };

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
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , dto.IsIndependent
                , dto.Status);

            return supplier;
        }

        Either<Error, Supplier> Add(Supplier supplier)
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

        Either<Error, Supplier> Save(Supplier supplier)
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

        Either<Error, SupplierDto> Update(Supplier supplier)
        {
            try
            {
                _supplierRepository.Save();
                return FromEntity(supplier);
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Supplier> Delete(Supplier supplier)
        {
            try
            {
                supplier.EditSupplier(supplier.Code
                    , supplier.Name
                    , supplier.Email
                    , supplier.Phone
                    , supplier.Rnc
                    , supplier.HomeOrApartment
                    , supplier.City
                    , supplier.Street
                    , supplier.Country
                    , supplier.IsIndependent
                    , Data.Entities.Common.EntityStatus.Deleted);
                return supplier;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
