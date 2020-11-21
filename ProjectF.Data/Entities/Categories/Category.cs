using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Categories
{
    public class Category : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public bool ShowOn { get; private set; }

        protected Category() { }

        public Category(Code code, Name name, bool showOn)
        {
            Code = code;
            Name = name;
            ShowOn = showOn;
        }

        public void EditCategory(Code code, Name name, bool showOn)
        {
            Code   = code;
            Name   = name;
            ShowOn = showOn;
        }

         public static implicit operator CategoryDto(Category category)
            => new CategoryDto(category.Id, category.Code.Value,category.Name.Value, category.ShowOn);
    }
}
