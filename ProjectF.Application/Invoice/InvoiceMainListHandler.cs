using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Entities.RequestFeatures;
using static ProjectF.Application.Validator.Validators;
using ProjectF.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Application.Invoice
{
    public class InvoiceMainListHandler
    {
        readonly InvoiceRepository _invoiceRepository;

        public InvoiceMainListHandler(InvoiceRepository invoiceRepository)
            => _invoiceRepository = invoiceRepository;


        public Task<Either<Error, (List<InvoiceListDto> List, MetaData Meta)>> Handle(InvoiceListParameters parameters)
        => ValidateDate(parameters)
            .MatchAsync(SuccAsync: (v) =>  GetInvoiceList(v),
                Fail: e => Error.New(string.Join("; ", e)));
        

        Validation<Error, InvoiceListParameters> ValidateDate(InvoiceListParameters parameters)
            => AtLeast(parameters.DateFrom)(parameters.DateTo).Map(c => parameters);

        Task<Either<Error, (List<InvoiceListDto>, MetaData)>> GetInvoiceList(InvoiceListParameters listParameters)
            => _invoiceRepository.GetInvoiceMainListAsync(listParameters, true)
                .MapT(c => (InvoiceList: c.Select(i => new InvoiceListDto(i.Id,
               i.InvoiceDate,
               i.DueDate,
               i.Total,
               0,
               i.Client,
               null)).ToList(),
               Metadata: c.MetaData));

    }
}
