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
using ProjectF.Application.Auth;
using ProjectF.Data.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ProjectF.Application.Companies
{
    public class CompanyCrudHandler
    {
        readonly CompanyRepository _companyRepository;
        readonly CountryRepository _countryRepository;
        readonly AuthUserCrudHandler _authUserCrudHandler;
        readonly IGetClaimsProvider _userData;
        readonly UserManager<User> _userManager;

        public CompanyCrudHandler(CompanyRepository companyRepository
            , CountryRepository countryRepository
            , AuthUserCrudHandler authUserCrud
            , IGetClaimsProvider userData
            , UserManager<User> userManager)
            => (_companyRepository
            , _countryRepository
            , _authUserCrudHandler
            , _userData
            , _userManager)
            = (companyRepository
            , countryRepository
            , authUserCrud
            , userData
            , userManager);

        public Task<Either<Error, Company>> Create(CompanyDto CompanyDto)
          => ValidateCompany(CompanyDto)
          .Bind(SetCountry)
          .Bind(SetStatus)
          .Bind(c => Add(FromDto(c)))
          .BindAsync(LimitCompanyCreation)
          .BindAsync(Save)
          .BindAsync(SetUserCompany);


        public Task<Either<Error, Company>> Update(long id, CompanyDto CompanyDto)
            => ValidateIsCorrectUpdate(id, CompanyDto)
            .Bind(ValidateCompany)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(CompanyDto, c))
            .BindAsync(Save);

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
            switch
            { true => dto, _ => Error.New("Invalid update entity id") };

        Either<Error, CompanyDto> SetStatus(CompanyDto dto)
            => dto with { Status = Data.Entities.Common.EntityStatus.Active };

        Either<Error, CompanyDto> ValidateCompany(CompanyDto companyDto)
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

        async Task<Either<Error, Company>> LimitCompanyCreation(Company entity)
        {
            var usera = await _userManager.FindByIdAsync(_userData.UserId);

            var company = _companyRepository
             .FindByCondition(c => c.CompanyId == usera.CompanyId, false)
             .FirstOrDefault();

            if ((company?.Id ?? 0) > 0)
                return Left<Error, Company>(Error.New("There is a company already created"));

            return entity;
        }

        async Task<Either<Error, Company>> SetUserCompany(Company company)
        {
            var result = await _authUserCrudHandler
                .UpdateUserCompany(company.Id, _userData.UserId);

            return result ? company : Error.New("Couldn't update user companyId");
        }

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

        async Task<Either<Error, Company>> Save(Company Company)
        {
            try
            {
                await _companyRepository.SaveAsync();
                _companyRepository.GetSavedEntry(Company);
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
