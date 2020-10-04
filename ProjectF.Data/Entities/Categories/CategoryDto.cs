using ProjectF.Data.Entities.Common.ValueObjects;
using System;

namespace ProjectF.Data.Entities.Categories
{
    public class CategoryDto
    {
        public long Id { get; }
        public string Code { get; }
        public string Name { get; }
        public bool ShowOn { get; }

        public CategoryDto(long id, string code, string name, bool showOn)
        {
            Id = id;
            Code = code;
            Name = name;
            ShowOn = showOn;
        }

        public CategoryDto With(long? id = null
            , string? code = null
            , string? name = null
            , bool? showOn = null)
            => new CategoryDto(id ?? this.Id
                , code ?? this.Code
                , name ?? this.Name
                , showOn ?? this.ShowOn);

        public static implicit operator Category(CategoryDto dto)
            =>  new Category(new Code(dto.Code), new Name(dto.Name), dto.ShowOn);
    }
}
