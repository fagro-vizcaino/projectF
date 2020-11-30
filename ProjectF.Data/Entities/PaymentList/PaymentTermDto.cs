using System;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.PaymentList
{
    public record PaymentTermDto
        (long Id, string Description, int DayValue, DateTime Created, DateTime? Modified, EntityStatus Status);
}
