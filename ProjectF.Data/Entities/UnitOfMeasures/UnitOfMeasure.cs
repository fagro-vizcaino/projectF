using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public class UnitOfMeasure : _BaseEntity
    {
        public Name Name { get; set; }
        public decimal Value { get; set; }
        protected UnitOfMeasure() { }

        public UnitOfMeasure(Name name
            , decimal value
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Name    = name;
            Value   = value;
            Created = created == DateTime.MinValue ? DateTime.Now : created;
            Status  = status;
        }

        public void Deconstruct(out Name dname
            , out decimal dvalue
            , out EntityStatus dstatus
            , out DateTime dcreated
            , out DateTime? dmodified)
        {
            dname = Name;
            dvalue = Value;
            dstatus = Status;
            dcreated = Created;
            dmodified = Modified;
        }

        public void EditUnitOfMeasure(Name name
            , decimal value
            , EntityStatus status)
        {
            Name = name;
            Value = value;
            Status = status;
            Modified = DateTime.Now;
        }
    }
}
