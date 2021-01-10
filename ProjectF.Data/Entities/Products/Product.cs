using System;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Data.Products
{
    public class Product : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public GeneralText Description { get; private set; }
        public GeneralText Reference { get; private set; }
        public virtual Category Category {get; private set;}
        public virtual Warehouse Warehouse { get; private set;}
        public virtual Tax Tax { get; private set;}
        public bool IsService { get; private set; }
        public decimal Cost { get; private set; }
        public decimal Price { get; private set; }
        public decimal Price2 { get; private set; }
        public decimal Price3 { get;  private set; }
        public decimal Price4 { get; private set; }


        protected Product() { }
        public Product(Code code
            , Name name
            , GeneralText description
            , GeneralText reference
            , Category category
            , Warehouse werehouse
            , Tax tax
            , bool isService
            , decimal cost
            , decimal price
            , decimal price2
            , decimal price3
            , decimal price4
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Code        = code;
            Name        = name;
            Description = description;
            Reference   = reference;
            Category    = category;
            Warehouse   = werehouse;
            Tax         = tax;
            IsService   = isService;
            Cost        = cost;
            Price       = price;
            Price2      = price2;
            Price3      = price3;
            Price4      = price4;
            Created     = created == DateTime.MinValue ? DateTime.Now : created;
            Status      = status;

        }

        public void EditProduct(Code code
            , Name name
            , GeneralText description
            , GeneralText reference
            , Category category
            , Warehouse werehouse
            , Tax tax
            , bool isService
            , decimal cost
            , decimal price
            , decimal price2
            , decimal price3
            , decimal price4
            , EntityStatus status)
        {
            Code        = code;
            Name        = name;
            Description = description;
            Reference   = reference;
            Category    = category;
            Warehouse   = werehouse;
            Tax         = tax;
            IsService   = isService;
            Cost        = cost;
            Price       = price;
            Price2      = price2;
            Price3      = price3;
            Price4      = price4;
            Status      = status;
            Modified    = DateTime.Now;
        }
    }
}
