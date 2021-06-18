using System.Collections.Generic;
using ProjectF.Application.Common;
using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.RequestFeatures;

namespace ProjectF.Application.Banks
{
    public class BankAccountMainListDto : IMainList<BankAccountDto>
    {
        public BankAccountMainListDto(IList<BankAccountDto> list, MetaData meta)
            => (List, Meta) = (list, meta);
        public IList<BankAccountDto> List { get; }
        public MetaData Meta { get; }
    }
}