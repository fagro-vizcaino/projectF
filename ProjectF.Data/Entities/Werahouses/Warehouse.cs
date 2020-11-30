using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Warehouses
{
    public class Warehouse : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public string Location { get; private set; }

        private readonly List<Product> _products = new List<Product>();
        public virtual IReadOnlyList<Product> Products => _products.ToList();
        protected Warehouse() { }

        public Warehouse(Code code, 
            Name name, 
            string location, 
            DateTime created, 
            EntityStatus status = EntityStatus.Active)
        {
            Code     = code;
            Name     = name;
            Location = location ?? string.Empty;
            Created  = created == DateTime.MinValue ? DateTime.Now : created;
            Status   = status;
        }

        public void EditWerehouse(Code code, Name name, string location, EntityStatus status)
        {
            Code     = code;
            Name     = name;
            Location = location ?? string.Empty;
            Modified = DateTime.Now;
            Status   = status;
        }
    }
}