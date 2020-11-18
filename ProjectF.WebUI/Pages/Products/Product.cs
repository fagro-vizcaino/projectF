﻿using ProjectF.WebUI.Models;
namespace ProjectF.WebUI.Pages.Products
{
    public class Product : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        public long WerehouseId { get; set; }
        public Warehouse Werehouse { get; set; }
        public long TaxId { get; set; }
        public Tax Tax { get; set; }
        public bool IsService { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Price4 { get; set; }
    }
}