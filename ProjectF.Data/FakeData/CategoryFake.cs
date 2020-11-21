using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Categories;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.FakeData
{
    public class CategoryFake: Category
    {
        public new long Id { get; set; }
        public new string Name { get; set; }
        public new string Code { get; set; }
        public new bool ShowOn { get; set; }
    }
}
