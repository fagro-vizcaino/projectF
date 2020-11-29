using System;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Api.Features.Categories
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool ShowOn { get; set; } = false;
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }
        public CategoryDto ToDto() => new CategoryDto(Id, Code, Name, ShowOn, Created, Modified, Status);
        public static CategoryViewModel FromDtoToView(CategoryDto category)
            => new CategoryViewModel()
            {
                Id = category.Id,
                Code = category.Code,
                Name = category.Name,
                ShowOn = category.ShowOn,
                Created = category.Created,
                Modified = category.Modified,
                Status = category.Status
            };

    }
}
