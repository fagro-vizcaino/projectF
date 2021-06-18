using System;
using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Data.Entities.Payments
{
    public class PaymentHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public virtual Client Client { get; private set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
