using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Categories
{
    public class Category : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public bool ShowOn { get; private set; }

        protected Category() { }

        public Category(Code code, Name name, bool showOn, DateTime created, EntityStatus status)
        {
            Code    = code;
            Name    = name;
            ShowOn  = showOn;
            Created = created == DateTime.MinValue ? DateTime.Now : created;
            Status  = status;
        }

        public void EditCategory(Code code, Name name, bool showOn, EntityStatus status)
        {
            Code     = code;
            Name     = name;
            ShowOn   = showOn;
            Modified = DateTime.Now;
            Status   = status;
        }
    }
}
