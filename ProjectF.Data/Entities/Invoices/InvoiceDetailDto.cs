namespace ProjectF.Data.Entities.Invoices
{
    public record InvoiceDetailDto
        (long Id, string ProductCode, string ProductDescription, int Qty, decimal Amount, decimal TaxPercent);   
}
