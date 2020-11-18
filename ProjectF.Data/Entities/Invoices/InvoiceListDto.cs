using ProjectF.Data.Entities.Clients;
using System;
using System.Globalization;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceListDto
    {
        public long InvoiceId { get; }
        public DateTime Date { get; }
        public string DisplayDate { get;}
        public string ClientName { get;}
        public string Status { get; }
        public DateTime DueDate { get; }
        public decimal Amount { get; }
        public decimal Balance { get; }
        
        private readonly Client _client;

        public InvoiceListDto(long invoiceId,
            DateTime date,
            DateTime dueDate,
            decimal amount,
            decimal balance,
            Client client,
            DateTime? paymentDate)
        {
            InvoiceId = invoiceId;
            Date      = date;
            DueDate   = dueDate;
            Amount    = amount;
            Balance   = balance;
            _client   = client;
            ClientName = GetClientName(_client);
            Status = GetStatus(dueDate, paymentDate);
            DisplayDate = DateDisplayFormat(date);
        }

        string DateDisplayFormat(DateTime date)
        {
            var dateFormat = date.ToString("MMM dd, yyyy", new CultureInfo("es-Es"));
            return char.ToUpper(dateFormat[0]) + dateFormat.Substring(1);
        }

        string GetClientName(Client client)
            => $"{client.Firstname.Value}";

        string GetStatus(DateTime invoiceDueDate, DateTime? paymentDate)
        {
            var paymentDay = paymentDate.GetValueOrDefault(DateTime.MinValue);
            if(paymentDay <= invoiceDueDate && Amount > Balance && Balance == 0)
                return $"Pendiente a {DateDisplayFormat(invoiceDueDate)}";
           
            if(paymentDay > invoiceDueDate && Amount > Balance && Balance == 0)
                return $"Vencido a {DateDisplayFormat(invoiceDueDate)}";

            if (Amount > Balance && Balance > 0)
               return $"Pacial";

            return "";
        }
    }
}
