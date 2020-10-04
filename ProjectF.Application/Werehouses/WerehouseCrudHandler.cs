using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Werehouses;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;

namespace ProjectF.Application.Werehouses
{
    public class WerehouseCrudHandler
    {
        readonly WerehouseRepository _werehouseRepository;

        public WerehouseCrudHandler(WerehouseRepository werehouseRepo)
        {
            _werehouseRepository = werehouseRepo;
        }

        public Either<Error, Werehouse> Create(WerehouseDto werehouseDto)
            => ValidateCode(werehouseDto)
            .Bind(ValidateName)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);


        public Either<Error, Werehouse> Update(long id, WerehouseDto werehouseDto)
            => ValidateIsCorrectUpdate(id, werehouseDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(werehouseDto, c))
            .Bind(Save);


        public Either<Error, Werehouse> Delete(long id)
           => Find(id)
             .Bind(Delete)
             .Bind(Save);

        public IEnumerable<WerehouseDto> GetAll()
            => _werehouseRepository.GetAll().Map(w => (WerehouseDto) w);

        public Either<Error, Werehouse> Find(params object[] valueKeys)
            => _werehouseRepository.Find(valueKeys).Match(Some: t => t,
             None: Left<Error, Werehouse>(Error.New("couldn't find werehouse type")));

        Either<Error, WerehouseDto> ValidateIsCorrectUpdate(long id, WerehouseDto werehouseDto)
        {
            if (id == werehouseDto.Id) return werehouseDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, WerehouseDto> ValidateCode(WerehouseDto werehouseDto)
            => Code.Of(werehouseDto.Code)
                .Match<Either<Error, WerehouseDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => werehouseDto);

        Either<Error, WerehouseDto> ValidateName(WerehouseDto werehosueDto)
            => Name.Of(werehosueDto.Name)
                .Match(Succ: c => werehosueDto,
                    Fail: err => Left<Error, WerehouseDto>(Error.New(string.Join(":", err))));

        Either<Error, Werehouse> CreateEntity(WerehouseDto werehouseDto)
            => Right<Error, Werehouse>(werehouseDto);

        Either<Error, Werehouse> UpdateEntity(WerehouseDto werehouseDto, Werehouse werehouse)
        {
            var code = new Code(werehouseDto.Code);
            var name = new Name(werehouseDto.Name);
            werehouse.EditWerehouse(code, name, werehouseDto.Location);
            return werehouse;
        }

        Either<Error, Werehouse> Add(Werehouse werehouse)
        {
            try
            {
                _werehouseRepository.Add(werehouse);
                return werehouse;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Werehouse> Save(Werehouse werehouse)
        {
            try
            {
                _werehouseRepository.Save();
                return werehouse;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Werehouse> Delete(Werehouse werehouse)
        {
            try
            {
                _werehouseRepository.Delete(werehouse);
                return werehouse;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}