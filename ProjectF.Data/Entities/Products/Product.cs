using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Entities.Werehouses;

namespace ProjectF.Data.Products
{
    public class Product : Entity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public GeneralText Description { get; private set; }
        public GeneralText Reference { get; private set; }
        public virtual Category Category {get; private set;}
        public virtual Werehouse Werehouse { get; private set;}
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
            , Werehouse werehouse
            , Tax tax
            , bool isService
            , decimal cost
            , decimal price
            , decimal price2
            , decimal price3
            , decimal price4)
        {
            Code        = code;
            Name        = name;
            Description = description;
            Reference   = reference;
            Category    = category;
            Werehouse   = werehouse;
            Tax         = tax;
            IsService   = isService;
            Cost        = cost;
            Price       = price;
            Price2      = price2;
            Price3      = price3;
            Price4      = price4;
        }

        public void EditProduct(Code code
            , Name name
            , GeneralText description
            , GeneralText reference
            , Category category
            , Werehouse werehouse
            , Tax tax
            , bool isService
            , decimal cost
            , decimal price
            , decimal price2
            , decimal price3
            , decimal price4)
        {
            Code        = code;
            Name        = name;
            Description = description;
            Reference   = reference;
            Category    = category;
            Werehouse   = werehouse;
            Tax         = tax;
            IsService   = isService;
            Cost        = cost;
            Price       = price;
            Price2      = price2;
            Price3      = price3;
            Price4      = price4;
        }
    }
}
