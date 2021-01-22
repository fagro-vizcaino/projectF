using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Categories
{
    public static class CategoryMapper
    {
        public static Category FromDto(CategoryDto dto)
            => new Category(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.ShowOn
                , dto.Created
                , dto.Status);

        public static CategoryDto FromEntity(Category entity)
            => (new CategoryDto(entity.Code.Value
                , entity.Name.Value
                , entity.ShowOn
                , entity.Created
                , entity.Modified)) with { Id = entity.Id, Status = entity.Status };
    }
}
