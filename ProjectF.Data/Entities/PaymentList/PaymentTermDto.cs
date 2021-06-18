using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.PaymentList
{
    public record PaymentTermDto(string Description, int DayValue) : FDto;
}
