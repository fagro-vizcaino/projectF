using ProjectF.Data.Entities.Categories;


namespace ProjectF.Api.Features.Category
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool ShowOn { get; set; } = false;

        public CategoryDto ToDto() => new CategoryDto(Id, Code, Name, ShowOn);
        public static CategoryViewModel FromDto(CategoryDto category)
            => new CategoryViewModel()
            {
                Id = category.Id,
                Code = category.Code,
                Name = category.Name,
                ShowOn = category.ShowOn
            };

    }
}
