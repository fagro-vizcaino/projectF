using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Warehouses
{
    public class Warehouse : Entity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public string Location { get; private set; }

        private readonly List<Product> _products = new List<Product>();
        public virtual IReadOnlyList<Product> Products => _products.ToList();
        protected Warehouse() { }

        public Warehouse(Code code, Name name, string location)
        {
            Code     = code;
            Name     = name;
            Location = location ?? string.Empty;
        }

        public void EditWerehouse(Code code, Name name, string location)
        {
            Code     = code;
            Name     = name;
            Location = location ?? string.Empty;
        }

        public static implicit operator WarehouseDto(Warehouse entity)
          => new WarehouseDto(entity.Id, entity.Code.Value, entity.Name.Value, entity.Location);
    }
}