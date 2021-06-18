using System;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Core;
using ProjectF.Data.Repositories;
using System.Collections.Generic;

namespace ProjectF.Application.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto">Dto to work with</typeparam>
    /// <typeparam name="TFEntity">The entity type you want to map to</typeparam>
    /// <typeparam name="TRepo">The Crud operations repository for this TFEntity</typeparam>
    public class BaseCrudHandler<TDto, TFEntity, TRepo>
        where TFEntity : _BaseEntity
        where TRepo : IBaseRepository<TFEntity>
        where TDto : FDto
    {
        private readonly TRepo _repo;

        protected BaseCrudHandler(TRepo repo) => _repo = repo;

        protected static Func<TDto, TFEntity> _fromDto;
        protected static Func<TFEntity, TDto> _fromEntity;
        protected static Func<TDto, TFEntity, Either<Error, TFEntity>> _updateEntity;

        public virtual Either<Error, TFEntity> Create(TDto dto)
            => SetStatus(dto)
            .Bind(c => Add(_fromDto(c)))
            .Bind(Save);

        public Either<Error, TFEntity> Delete(long id)
            => Find(id)
            .Bind(Delete)
            .Bind(Save);

        public virtual Either<Error, TFEntity> Update(long id, TDto dto)
            => ValidateIsCorrectUpdate(id, dto)
            .Bind(c => Find(c.Id))
            .Bind(c => _updateEntity(dto, c))
            .Bind(Save);

        public IEnumerable<TDto> GetAll()
            => _repo.GetAll()
                .Map(t => _fromEntity(t));

        Either<Error, TDto> SetStatus(TDto dto) => dto with { Status = EntityStatus.Active };

        Either<Error, TDto> ValidateIsCorrectUpdate(long id, TDto dto)
            => (id == dto.Id) switch { true => dto, _ => Error.New("Invalid update entity id") };

        public Either<Error, TFEntity> Find(params object[] valueKeys)
           => _repo.Find(valueKeys).Match(Some: p => p,
                None: Left<Error, TFEntity>(Error.New($"Couldn't find {nameof(TFEntity)}")));

        private Either<Error, TFEntity> Add(TFEntity entity)
        {
            try
            {
                _repo.Add(entity);
                return entity;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private Either<Error, TFEntity> Save(TFEntity entity)
        {
            try
            {
                _repo.Save();
                return entity;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        private Either<Error, TFEntity> Delete(TFEntity entity)
        {
            entity.SetStatus(EntityStatus.Deleted);
            return entity;
        }
    }
}
