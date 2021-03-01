using System;

namespace ProjectF.WebUI.Models
{
    public class Supplier : FEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Rnc { get; set; }
        public string HomeOrApartment { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int SelectedCountry { get; set; }
        public Country Country { get; set; }
        public bool IsIndependent { get; set; }
        public SupplierGroup SupplierGroup { get; set; }
        public int PaymentTermId { get; set; }
        public PaymentTerm PaymentTerm { get; set;}
        public string Notes { get; set; }
    }

    public enum SupplierGroup
    {
        National = 1,
        International
    }
}
