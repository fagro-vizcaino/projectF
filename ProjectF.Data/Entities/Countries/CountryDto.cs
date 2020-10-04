namespace ProjectF.Data.Entities.Countries
{
    public class CountryDto
    {
        public long Id { get; set; }
        public string Name { get; }
        public string IconImage { get; }

        public CountryDto(long id, string name, string IconImage)
        {
            Id = id;
            Name = name;
            this.IconImage = IconImage;
        }

        public CountryDto With(long? id = null
            , string? name = null
            , string? iconImage = null)
            => new CountryDto(id ?? this.Id
                , name ?? this.Name
                , iconImage ?? this.IconImage);


    }
}