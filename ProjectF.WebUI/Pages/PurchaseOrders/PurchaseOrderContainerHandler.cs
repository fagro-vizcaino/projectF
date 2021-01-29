using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Components.Common;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrderContainerHandler : BaseContainerBasicCrud<PurchaseOrder>
    {
        public PurchaseOrderContainerHandler() : base("OrderCompra")
        {
          InitModel(GetNewModelOrEdit());
          NewOrEditOperation = GetNewModelOrEdit;
        }


        public PurchaseOrder GetNewModelOrEdit(PurchaseOrder purchaseOrder = null)
        {
            return new PurchaseOrder();
        }
    }
}
