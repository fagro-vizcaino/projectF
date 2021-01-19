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
    public class BaseCrudHandler<Dto, Fentity, Repo>
        where Fentity : _BaseEntity
        where Repo : IBaseRepository<Fentity>
        where Dto : FDto
    {
        readonly Repo _repo;

        public BaseCrudHandler(Repo repo) => _repo = repo;
        
        public static Func<Dto, Fentity> fromDto;
        public static Func<Fentity, Dto> fromEntity;
        public static Func<Dto, Fentity, Either<Error, Fentity>> updateEntity;

        public virtual Either<Error, Fentity> Create(Dto dto)
            => SetStatus(dto)
            .Bind(c => Add(fromDto(c)))
            .Bind(Save);

        public Either<Error, Fentity> Delete(long id)
            => Find(id)
            .Bind(Delete)
            .Bind(Save);

        public Either<Error, Fentity> Update(long id, Dto dto)
            =>ValidateIsCorrectUpdate(id, dto)
            .Bind(c => Find(c.Id))
            .Bind(c => updateEntity(dto, c))
            .Bind(Save);


        public IEnumerable<Dto> GetAll()
            => _repo.GetAll()
                .Map(t => fromEntity(t));

        Either<Error, Dto> SetStatus(Dto dto) => dto with { Status = EntityStatus.Active };

        Either<Error, Dto> ValidateIsCorrectUpdate(long id, Dto dto)
            => (id == dto.Id) switch { true => dto, _ => Error.New("Invalid update entity id") };

        public Either<Error, Fentity> Find(params object[] valueKeys)
           => _repo.Find(valueKeys).Match(Some: p => p,
                None: Left<Error, Fentity>(Error.New($"Couldn't find {nameof(Fentity)}")));

        protected virtual Either<Error, Fentity> Add(Fentity entity)
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

        protected virtual Either<Error, Fentity> Save(Fentity entity)
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

        protected virtual Either<Error, Fentity> Delete(Fentity entity)
        {
            entity.SetStatus(EntityStatus.Deleted);
            return entity;
        }
    }
}
