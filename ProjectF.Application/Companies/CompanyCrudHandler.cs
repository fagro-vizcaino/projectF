using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;
using ProjectF.Data.Repositories;
using LanguageExt.Common;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Companies;
using static ProjectF.Data.Entities.Companies.CompanyMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Application.Companies
{
    public class CompanyCrudHandler
    {
        readonly CompanyRepository _companyRepository;
        readonly CountryRepository _countryRepository;

        public CompanyCrudHandler(CompanyRepository companyRepository
            , CountryRepository countryRepository)
            => (_companyRepository, _countryRepository) = (companyRepository, countryRepository);

        public Either<Error, Company> Create(CompanyDto CompanyDto)
          => validateCompany(CompanyDto)
          .Bind(SetCountry)
          .Bind(SetStatus)
          .Bind(c => Add(FromDto(c)))
          .Bind(Save);


        public Either<Error, Company> Update(long id, CompanyDto CompanyDto)
            => ValidateIsCorrectUpdate(id, CompanyDto)
            .Bind(validateCompany)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(CompanyDto, c))
            .Bind(Save);

        //public Either<Error, Company> Delete(long id)
        //  => Find(id)
        //    .Bind(Delete)
        //    .Bind(Save);

        public IEnumerable<CompanyDto> GetAll()
            => _companyRepository.GetAll().Map(t => FromEntity(t));

        public Either<Error, Company> Find(params object[] valueKeys)
           => _companyRepository.Find(valueKeys).Match(Some: p => p,
                None: Left<Error, Company>(Error.New("Couldn't find Company")));

        ////Missing Pagination

        Either<Error, CompanyDto> ValidateIsCorrectUpdate(long id, CompanyDto dto)
            => (id == dto.Id) 
            switch { true => dto, _ => Error.New("Invalid update entity id")};

        Either<Error, CompanyDto> SetStatus(CompanyDto dto) 
            => dto with { Status = Data.Entities.Common.EntityStatus.Active };

        Either<Error, CompanyDto> validateCompany(CompanyDto companyDto)
            => new CompanyValidator().Validate(companyDto).IsValid 
            ? companyDto
            : Error.New(string.Join(";", new CompanyValidator()
                .Validate(companyDto)
                .Errors.Select(c => c.ErrorMessage)));

        Either<Error, CompanyDto> SetCountry(CompanyDto dto)
            => _countryRepository.FromCountryId(dto.Country.Id) switch
            {
                Country n => dto with { Country = n },
                _ => Error.New("couldn't find country"),
            };

        Either<Error, Company> UpdateEntity(CompanyDto dto, Company Company)
        {
            Company.EditCompany(new Name(dto.Name)
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , new Phone(dto.Phone)
                , dto.Website
                , dto.Status);

            return Company;
        }

        Either<Error, Company> Add(Company company)
        {
            try
            {
                _companyRepository.Add(company);
                return company;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Company> Save(Company Company)
        {
            try
            {
                _companyRepository.Save();
                return Company;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        //Either<Error, Tax> Delete(Tax tax)
        //{
        //    try
        //    {
        //        _taxRepository.Delete(tax);
        //        return tax;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Error.New($"{ex.Message}\n{ex.StackTrace}");
        //    }
        //}

    }
}
