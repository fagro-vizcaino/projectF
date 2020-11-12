using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentList
{
    public record PaymentTermDto
    {
        public long Id { get; }
        public string Description { get; }
        public int DayValue { get; }

        public PaymentTermDto(long id, string description, int dayValue)
         => (Id, Description, DayValue) = (id, description, dayValue);

        public static implicit operator PaymentTerm(PaymentTermDto dto)
            =>  new PaymentTerm(new Name(dto.Description), dto.DayValue);
    }
}
