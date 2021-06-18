using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentList
{
    /// <summary>
    /// 
    /// </summary>
    public class PaymentTerm : _BaseEntity
    {
        public Name Description { get; private set; }
        public int DayValue { get; private set; }

        protected PaymentTerm() { }

        public PaymentTerm(Name description, int dayValue, DateTime created, EntityStatus status = EntityStatus.Active)
        {
            Description = description;
            DayValue    = dayValue;
            Created     = created == DateTime.MinValue ? DateTime.Now : created;
            Status      = status;
        }

        public void EditPaymentTerm(Name description, int dayValue, EntityStatus status)
        {
            Description = description;
            DayValue    = dayValue;
            Status      = status;
            Modified    = DateTime.Now;
        }
    }
}
