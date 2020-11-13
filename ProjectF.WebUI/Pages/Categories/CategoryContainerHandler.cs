using ProjectF.WebUI.Models;
using ProjectF.WebUI.Components.Common;

namespace ProjectF.WebUI.Pages.Categories
{
    public class CategoryContainerHandler : BaseContainerBasicCrud<Category>
    {
        public CategoryContainerHandler() : base("Categoria")
        {
            var emptyModel = new Category
            {
                Id = 0,
                Name = string.Empty,
                Code = string.Empty,
                ShowOn = false
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;

        }

        public Category GetNewModelOrEdit(Category category = null)
            => category != null
                ? new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Code = category.Code,
                    ShowOn = category.ShowOn
                }
                : new Category { Id = 0, Name = null, Code = GenerateCode, ShowOn = false };

    }
}
