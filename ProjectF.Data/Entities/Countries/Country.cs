namespace ProjectF.Data.Entities.Countries
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconImage { get; set; }

        protected Country() { }

        public Country(int id, string name, string iconImage)
        {
            Id          = id;
            Name        = name;
            IconImage   = iconImage;
        }
    }
}