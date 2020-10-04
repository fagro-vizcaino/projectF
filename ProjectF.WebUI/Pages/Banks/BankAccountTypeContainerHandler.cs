using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Banks
{
    public class BankAccountTypeContainerHandler : BaseContainerBasicCrud<BankAccountType>
    {
        public BankAccountTypeContainerHandler() : base("Tipo Cuenta")
        {
            var emptyModel = new BankAccountType
            {
                Id = 0,
                Name = string.Empty,
                Description = string.Empty
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }



        public BankAccountType GetNewModelOrEdit(BankAccountType bankAccountType = null)
            => bankAccountType != null
            ? new BankAccountType
            {
                Id = bankAccountType.Id,
                Name = bankAccountType.Name,
                Description = bankAccountType.Description
            }
            : new BankAccountType { Id = 0, Name = string.Empty, Description = string.Empty};
    }
}
