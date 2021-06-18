using ProjectF.WebUI.Models;
using ProjectF.WebUI.Pages.UnitOfMeasures;

namespace ProjectF.WebUI.Pages.Products
{
    public class Product : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public  UnitOfMeasure UnitOfMeasure { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int TaxId { get; set; }
        public Tax Tax { get; set; }
        public bool IsService { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
    }
}