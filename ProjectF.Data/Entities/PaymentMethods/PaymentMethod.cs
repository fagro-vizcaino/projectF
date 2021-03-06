﻿using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentMethods
{
    public class PaymentMethod : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name Description { get; private set; }

        protected PaymentMethod() { }

        public PaymentMethod(Code code, 
            Name description, 
            DateTime created, 
            EntityStatus status = EntityStatus.Active)
        {
            Code        = code;
            Description = description;
            Created     = created == DateTime.MinValue ? DateTime.Now : created;
            Status      = status;
        }

        public void EditPaymentMethod(Name description, EntityStatus status)
        {
            Description = description;
            Modified    = DateTime.Now;
            Status      = status;
        }
    }
}
