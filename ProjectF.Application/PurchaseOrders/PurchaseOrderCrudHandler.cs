using ProjectF.Application.Common;
using ProjectF.Data.Entities.PurchaseOrders;
using static ProjectF.Data.Entities.PurchaseOrders.PurchaseOrderMapper;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Application.PurchaseOrders
{
    public class PurchaseOrderCrudHandler : BaseCrudHandler<PurchaseOrderHeaderDto, PurchaseOrderHeader, PurchaseOrderRepository>
    {
        readonly PurchaseOrderRepository _purchaseOrderRepository;

        public PurchaseOrderCrudHandler(PurchaseOrderRepository repository) : base(repository)
            => (_purchaseOrderRepository, fromDto, fromEntity, updateEntity) 
            = (repository, FromDto, FromEntity, UpdateEntity);

        Either<Error, PurchaseOrderHeader> UpdateEntity(PurchaseOrderHeaderDto dto, PurchaseOrderHeader purchaseOrderHeader)
        {
            purchaseOrderHeader.EditPurchaseOrderHeader(new Code(dto.Code)
                , new Name(dto.SupplierName)
                , dto.SupplierId
                , dto.Rnc
                , new GeneralText(dto.Address)
                , dto.DeliverDate
                , new Name(dto.PaymentTermName)
                , dto.PaymentTermId 
                , new Name(dto.WarehouseName)
                , dto.WarehouseId
                , new GeneralText(dto.Notes)
                , dto.SubTotal
                , dto.TaxTotal
                , dto.DiscountTotal
                , dto.Total
                , dto.PurcharseOrderDetail.Map(c => FromDto(c)).ToList());

            return purchaseOrderHeader;

        }


        public Task<Either<Error, PurchaseOrderHeader>> FindAsync(params object[] valueKeys)
            => _purchaseOrderRepository.FindByAsync(valueKeys);
    }
}
