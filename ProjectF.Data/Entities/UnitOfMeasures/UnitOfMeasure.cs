using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public class UnitOfMeasure : _BaseEntity
    {
        public Name Name { get; set; }

        protected UnitOfMeasure() { }

        public UnitOfMeasure(Name name
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Name    = name;
            Created = created == DateTime.MinValue ? DateTime.Now : created;
            Status  = status;
        }

        public void Deconstruct(out Name dname
            , out EntityStatus dstatus
            , out DateTime dcreated
            , out DateTime? dmodified)
        {
            dname = Name;
            dstatus = Status;
            dcreated = Created;
            dmodified = Modified;
        }

        public void EditUnitOfMeasure(Name name
            , EntityStatus status)
        {
            Name = name;
            Status = status;
            Modified = DateTime.Now;
        }
    }
}
