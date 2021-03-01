using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Categories
{
    public static class CategoryMapper
    {
        public static Category FromDto(CategoryDto dto)
            =>  new(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.ShowOn
                , dto.Created
                , dto.Status);

        public static CategoryDto FromEntity(Category entity)
            => new(entity.Code.Value
                , entity.Name.Value
                , entity.ShowOn)
            {
                Id = entity.Id, 
                Status = entity.Status, 
                Created = entity.Created,
                Modified = entity.Modified
            };
    }
}
