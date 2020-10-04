using System;
using System.Collections.Generic;
using System.Text;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Taxes
{
    public class TaxRegimeTypeCrudHandler
    {
        readonly TaxRegimeTypeRepository _taxRegimeTypeRepository;

        public TaxRegimeTypeCrudHandler(TaxRegimeTypeRepository taxRegimeTypeRepository)
        {
            _taxRegimeTypeRepository = taxRegimeTypeRepository;
        }

        public Either<Error, TaxRegimeType> Create(TaxRegimeDto taxRegimeDto)
            => ValidateName(taxRegimeDto)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);

        public Either<Error, TaxRegimeType> Update(long id, TaxRegimeDto taxDto)
            => ValidateIsCorrectUpdate(id, taxDto)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(taxDto, c))
            .Bind(Save);

        public IEnumerable<TaxRegimeDto> GetAll()
            => _taxRegimeTypeRepository.GetAll().Map(t => (TaxRegimeDto) t);

        public Either<Error, TaxRegimeType> Find(params object[] valueKeys)
         => _taxRegimeTypeRepository.Find(valueKeys).Match(Some: t => t, 
             None: Left<Error, TaxRegimeType>(Error.New("couldn't find tax regime type")));
        
        Either<Error, TaxRegimeDto> ValidateIsCorrectUpdate(long id, TaxRegimeDto taxRegimeDto)
        {
            if (id == taxRegimeDto.Id) return taxRegimeDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, TaxRegimeDto> ValidateName(TaxRegimeDto taxRegimeDto)
            => Name.Of(taxRegimeDto.Name).Match(Succ: c => taxRegimeDto,
                 Fail: err => Left<Error, TaxRegimeDto>(Error.New(string.Join(";", err))));

        Either<Error, TaxRegimeType> CreateEntity(TaxRegimeDto taxRegimeDto)
            => Right<Error, TaxRegimeType>(taxRegimeDto);

        Either<Error, TaxRegimeType> UpdateEntity(TaxRegimeDto taxRegimeDto, TaxRegimeType taxRegime)
        {
            taxRegime.EditTaxRegimeType(new Name(taxRegimeDto.Name), new GeneralText(taxRegimeDto.Description));
            return taxRegime;
        }

        Either<Error, TaxRegimeType> Add(TaxRegimeType taxRegime)
        {
            try
            {
                _taxRegimeTypeRepository.Add(taxRegime);
                return taxRegime;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, TaxRegimeType> Save(TaxRegimeType tax)
        {
            try
            {
                _taxRegimeTypeRepository.Save();
                return tax;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
