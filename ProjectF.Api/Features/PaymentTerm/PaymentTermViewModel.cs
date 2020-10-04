using ProjectF.Data.Entities.PaymentList;
using ProjectF.Application.PaymentTerms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Api.Features.PaymentTerms
{
    public class PaymentTermViewModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int DayValue { get; set; }

        public PaymentTermDto ToDto() => new PaymentTermDto(Id, Description, DayValue);
        public static PaymentTermViewModel FromDto(PaymentTermDto paymentTermDto)
            => new PaymentTermViewModel()
            {
                Id          = paymentTermDto.Id,
                DayValue    = paymentTermDto.DayValue,
                Description = paymentTermDto.Description
            };
    }
}
