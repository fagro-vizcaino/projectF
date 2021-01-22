using ProjectF.WebUI.Components.Common;

namespace ProjectF.WebUI.Pages.PaymentMethods
{
    public class PaymentMethodContainerHandler : BaseContainerBasicCrud<PaymentMethod>
    {
        public PaymentMethodContainerHandler() : base("Método de pago")
        {
            var emptyObject = new PaymentMethod
            {
                Code = GenerateCode,
                Description = string.Empty,
                Id = 0
            };
            InitModel(emptyObject);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        public PaymentMethod GetNewModelOrEdit(PaymentMethod paymentMethod = null)
         => paymentMethod ?? new PaymentMethod 
            {
                Id          = 0,
                Description = string.Empty,
                Code        = GenerateCode
            };
    }
}
