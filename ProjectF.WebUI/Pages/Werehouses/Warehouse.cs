using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Products;

namespace ProjectF.WebUI.Models
{
    public class Warehouse : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public IReadOnlyList<Product> Products { get; set; }
    }
}
