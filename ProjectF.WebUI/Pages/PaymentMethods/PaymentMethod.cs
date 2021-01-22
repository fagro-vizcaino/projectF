using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.PaymentMethods
{
    public class PaymentMethod : FEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
