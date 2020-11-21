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

        public PaymentTerm(Name description, int dayValue)
        {
            Description = description;
            DayValue    = dayValue;
        }

        public void EditPaymentDeadlines(Name description, int dayValue)
        {
            Description = description;
            DayValue    = dayValue;
        }

         public static implicit operator PaymentTermDto(PaymentTerm entity)
            =>  new PaymentTermDto(entity.Id, entity.Description.Value, entity.DayValue);
    }
}
