using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceDetailDto
    {
        public long Id { get; }
        public string ProductCode { get; }
        public string ProductDescription { get; }
        public int Qty { get; }
        public decimal Amount { get; }
        public decimal TaxPercent { get; }

        public InvoiceDetailDto(long id,
            string productCode,
            string productDescription,
            int qty,
            decimal amount,
            decimal taxPercent)
            => (Id, ProductCode, ProductDescription, Qty, Amount, TaxPercent)
                = (id, productCode, productDescription, qty, amount, taxPercent);


        public InvoiceDetailDto With(long? id = null,
            string? productCode               = null,
            string? productDescription        = null,
            int? qty                          = null,
            decimal? amount                   = null,
            decimal? taxPercent               = null)
                                              => new InvoiceDetailDto(id ?? this.Id
                , productCode ?? this.ProductCode
                , productDescription ?? this.ProductDescription
                , qty ?? this.Qty
                , amount ?? this.Amount
                , taxPercent ?? this.TaxPercent);
    }
}
