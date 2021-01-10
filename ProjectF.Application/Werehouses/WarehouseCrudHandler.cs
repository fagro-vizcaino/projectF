using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Warehouses;
using static ProjectF.Data.Entities.Warehouses.WarehouseMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;

namespace ProjectF.Application.Werehouses
{
    public class WarehouseCrudHandler
    {
        readonly WerehouseRepository _werehouseRepository;

        public WarehouseCrudHandler(WerehouseRepository werehouseRepo)
        {
            _werehouseRepository = werehouseRepo;
        }

        public Either<Error, Warehouse> Create(WarehouseDto werehouseDto)
            => ValidateCode(werehouseDto)
            .Bind(ValidateName)
            .Bind(c => Add(FromDto(c)))
            .Bind(Save);

        public Either<Error, Warehouse> Update(long id, WarehouseDto werehouseDto)
            => ValidateIsCorrectUpdate(id, werehouseDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(werehouseDto, c))
            .Bind(Save);

        public Either<Error, Warehouse> Delete(long id)
           => Find(id)
             .Bind(Delete)
             .Bind(Save);

        public IEnumerable<WarehouseDto> GetAll()
            => _werehouseRepository.GetAll().Map(w => FromEntity(w));

        public Either<Error, Warehouse> Find(params object[] valueKeys)
            => _werehouseRepository.Find(valueKeys).Match(Some: t => t,
             None: Left<Error, Warehouse>(Error.New("couldn't find werehouse type")));

        Either<Error, WarehouseDto> ValidateIsCorrectUpdate(long id, WarehouseDto werehouseDto)
        {
            if (id == werehouseDto.Id) return werehouseDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, WarehouseDto> ValidateCode(WarehouseDto werehouseDto)
            => Code.Of(werehouseDto.Code)
                .Match<Either<Error, WarehouseDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => werehouseDto);

        Either<Error, WarehouseDto> ValidateName(WarehouseDto werehosueDto)
            => Name.Of(werehosueDto.Name)
                .Match(Succ: c => werehosueDto,
                    Fail: err => Left<Error, WarehouseDto>(Error.New(string.Join(":", err))));

        Either<Error, Warehouse> UpdateEntity(WarehouseDto dto, Warehouse werehouse)
        {
            var code = new Code(dto.Code);
            var name = new Name(dto.Name);
            werehouse.EditWerehouse(code, name, dto.Location, dto.Status);
            return werehouse;
        }

        Either<Error, Warehouse> Add(Warehouse werehouse)
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

        Either<Error, Warehouse> Save(Warehouse werehouse)
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

        Either<Error, Warehouse> Delete(Warehouse warehouse)
        {
            try
            {
                warehouse.EditWerehouse(warehouse.Code
                    , warehouse.Name
                    , warehouse.Location
                    , Data.Entities.Common.EntityStatus.Deleted);
                return warehouse;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}