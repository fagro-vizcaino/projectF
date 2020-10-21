namespace ProjectF.WebUI.Models
{
    public class Tax : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Percentvalue { get; set; }
    }
}
