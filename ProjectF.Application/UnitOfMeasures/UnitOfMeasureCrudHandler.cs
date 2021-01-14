using System;
using System.Collections.Generic;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.UnitOfMeasures;
using ProjectF.Data.Repositories;
using static LanguageExt.Prelude;
using static ProjectF.Data.Entities.UnitOfMeasures.UnitOfMeasureMapper;

namespace ProjectF.Application.UnitOfMeasures
{
    public class UnitOfMeasureCrudHandler
    {
        readonly UnitOfMeasureRepository _ofMeasureRepository;

        public UnitOfMeasureCrudHandler(UnitOfMeasureRepository unitOfMeasureRepository)
            => _ofMeasureRepository = unitOfMeasureRepository;

        public Either<Error, UnitOfMeasureDto> Create(UnitOfMeasureDto dto)
            => SetStatus(dto)
            .Bind(c => Add(FromDto(c)))
            .Bind(Save);

        public Either<Error, UnitOfMeasureDto> Update(long id, UnitOfMeasureDto dto)
            => ValidateIsCorrectUpdate(id, dto)
            .Bind(s => Find(s.Id))
            .Bind(s => UpdateEntity(dto, s))
            .Bind(Save);

        public IEnumerable<UnitOfMeasureDto> FindAll()
            => _ofMeasureRepository.GetAll().Map(c => FromEntity(c));

        public Either<Error, UnitOfMeasure> Find(params object[] valueKeys)
           => _ofMeasureRepository.Find(valueKeys).Match(Some: p => p,
                None: Left<Error, UnitOfMeasure>(Error.New("Couldn't find Unit Of measure")));

        public Either<Error, UnitOfMeasureDto> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        Either <Error, UnitOfMeasure> Add(UnitOfMeasure measure)
        {
            try
            {
                _ofMeasureRepository.Add(measure);
                return measure;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, UnitOfMeasureDto> ValidateIsCorrectUpdate(long id, UnitOfMeasureDto dto)
        {
            if (id == dto.Id) return dto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, UnitOfMeasureDto> Save(UnitOfMeasure measure)
        {
            try
            {
                _ofMeasureRepository.Save();
                return FromEntity(measure);
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, UnitOfMeasure> UpdateEntity(UnitOfMeasureDto dto, UnitOfMeasure unitOfMeasure)
        {
           unitOfMeasure.EditUnitOfMeasure(new Name(dto.Name), dto.Value, dto.Status);
           return unitOfMeasure;
        }

        Either<Error, UnitOfMeasureDto> SetStatus(UnitOfMeasureDto dto)
           => dto with { Status = EntityStatus.Active };

        Either<Error, UnitOfMeasure> Delete(UnitOfMeasure measure)
        {
            try
            {
                measure.EditUnitOfMeasure(measure.Name, measure.Value, EntityStatus.Deleted);
                return measure;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
