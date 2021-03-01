using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Categories;
using static ProjectF.Application.Categories.CategoryMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Application.Common;

namespace ProjectF.Application.Categories
{
    public class CategoryCrudHandler : BaseCrudHandler<CategoryDto, Category, CategoryRepository>
    {
        readonly CategoryRepository _categoryRepository;
        public CategoryCrudHandler(CategoryRepository categoryRepository): base(categoryRepository)
            => (_categoryRepository, _fromDto, _fromEntity, _updateEntity) = (categoryRepository, FromDto, FromEntity, UpdateEntity);
        
        Either<Error, Category> UpdateEntity(CategoryDto dto, Category category)
        {
            category.EditCategory(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.ShowOn
                , dto.Status);
            return category;
        }

    }
}