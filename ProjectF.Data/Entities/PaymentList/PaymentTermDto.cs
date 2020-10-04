using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentList
{
    public class PaymentTermDto
    {
        public long Id { get; }
        public string Description { get; }
        public int DayValue { get;}

        public PaymentTermDto(long id, string description, int dayValue)
         => (Id, Description, DayValue) = (id, description, dayValue);

        public void Deconstruct(out long id, out string description, out int dayValue)
            => (id, description, dayValue) = (Id, Description, DayValue);

        public PaymentTermDto With(long? id = null, string? description = null, int? dayValue = null)
            => new PaymentTermDto(id ?? this.Id, 
                description ?? this.Description, 
                dayValue ?? this.DayValue);

        public static implicit operator PaymentTerm(PaymentTermDto dto)
            =>  new PaymentTerm(new Name(dto.Description), dto.DayValue);

    }
}
