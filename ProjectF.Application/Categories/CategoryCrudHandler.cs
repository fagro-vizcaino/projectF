using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Categories;
using static ProjectF.Data.Entities.Categories.CategoryMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ProjectF.Application.Categories
{
    public class CategoryCrudHandler
    {
        readonly CategoryRepository _categoryRepository;
        public CategoryCrudHandler(CategoryRepository categoryRepository)
            => _categoryRepository = categoryRepository;

        public Either<Error, Category> Create(CategoryDto categoryDto)
            => ValidateCode(categoryDto)
            .Bind(ValidateName)
            .Bind(c => Add(FromDto(c)))
            .Bind(Save);

        public Either<Error, Category> Update(long id, CategoryDto categoryDto)
            => Exists(id, categoryDto)
            .Bind(ValidateCode)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(categoryDto, c))
            .Bind(Save);

        public Either<Error, Category> Delete(long id)
            => Find(id)
              .Bind(Delete)
              .Bind(Save);        

        public IEnumerable<CategoryDto> GetAll()
            => _categoryRepository.GetAll().Map(ct => FromEntity(ct));

        public Either<Error, Category> Find(params object[] valueKeys)
            => _categoryRepository.Find(valueKeys)
            .Match(Some:c => Right(c), 
                None: () => Left<Error, Category>(Error.New("Category no found")));
  
        Either<Error, CategoryDto> Exists(long id, CategoryDto categoryDto)
        {
            if (id == categoryDto.Id) return categoryDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, CategoryDto> ValidateCode(CategoryDto categoryDto)
            => Code.Of(categoryDto.Code)
                .Match<Either<Error, CategoryDto>>(
                    Left: err => Error.New(err.Message),
                    Right: c => categoryDto);

        Either<Error, CategoryDto> ValidateName(CategoryDto categoryDto)
            => Name.Of(categoryDto.Name)
                .Match(Succ: c => categoryDto,
                    Fail: err => Left<Error, CategoryDto>(Error.New(string.Join(";", err))));

        Either<Error, Category> UpdateEntity(CategoryDto dto, Category category)
        {
            category.EditCategory(new Code(dto.Code)
                , new Name(dto.Name)
                , dto.ShowOn
                , dto.Status);
            return category;
        }

        Either<Error, Category> Add(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return category;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Category> Save(Category category)
        {
            try
            {
                _categoryRepository.Save();
                return category;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Category> Delete(Category category)
        {
            try
            {
                _categoryRepository.Delete(category);
                return category;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}