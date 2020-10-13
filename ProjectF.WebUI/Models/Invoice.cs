﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class Invoice : FEntity
    {
        public string Code { get; set; }
        public string Ncf { get; set; }
        public string Rnc { get; set; }
        public string Email { get;set;}
        public Client Client { get; set; }
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public long PaymentTermId { get; set; }
        public string Notes { get; set; }
        public string TermAndConditions { get; set; }
        public string Footer { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
