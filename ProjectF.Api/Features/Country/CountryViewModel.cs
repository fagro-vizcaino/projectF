using ProjectF.Data.Entities.Countries;
using ProjectF.Application.Countries;

namespace ProjectF.Api.Features.Countries
{
    public class CountryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconImage { get; set; } = string.Empty;

        public CountryDto ToDto() => new CountryDto(Id, Name, IconImage);
        public static CountryViewModel FromDto(CountryDto country)
            => new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                IconImage = country.IconImage
            };
    }
}