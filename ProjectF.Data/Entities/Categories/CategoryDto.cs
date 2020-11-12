using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Categories
{
    public record CategoryDto
    {
        public long Id { get; }
        public string Code { get; }
        public string Name { get; }
        public bool ShowOn { get; }

        public CategoryDto(long id, string code, string name, bool showOn)
            =>(Id, Code, Name, ShowOn) = (id, code, name, showOn);
    
        public static implicit operator Category(CategoryDto dto)
            =>  new Category(new Code(dto.Code), new Name(dto.Name), dto.ShowOn);
    }
}
