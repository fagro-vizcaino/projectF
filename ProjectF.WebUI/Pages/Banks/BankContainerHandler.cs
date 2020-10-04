using FluentValidation;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Banks
{
    public class BankContainerHandler : BaseContainerBasicCrud<Bank>
    {
        public BankContainerHandler() : base("Banco")
        {
            var emptyModel = new Bank
            {
                Id = 0,
                AccountName = string.Empty,
                AccountNumber = string.Empty,
                BankAccountTypeId = 0,
                BankAccountType = null
            };
            InitModel(emptyModel);
            NewOrEditOperation = GetNewModelOrEdit;
        }
        [Inject]
        public IBaseDataService<BankAccountType> BankAccountTypeData { get; set; }
        public BankAccountType[] BankAccountTypes { get; set; } = Array.Empty<BankAccountType>();
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();
           
            BankAccountTypes = (await BankAccountTypeData.GetAll()).ToArray();
        }

        public Bank GetNewModelOrEdit(Bank bank = null)
            => bank != null
                ? new Bank
                {
                    Id                = bank.Id,
                    AccountName       = bank.AccountName,
                    AccountNumber     = bank.AccountNumber,
                    BankAccountTypeId = bank.BankAccountTypeId,
                    BankAccountType   = bank.BankAccountType,
                    Description       = bank.Description,
                    InitialBalance    = bank.InitialBalance,
                    Created           = bank.Created,
                    Modified          = DateTime.UtcNow
                }
                : new Bank
                {
                    Id                = 0,
                    AccountName       = string.Empty,
                    AccountNumber     = string.Empty,
                    BankAccountTypeId = 0,
                    BankAccountType   = new BankAccountType(),
                    Description       = string.Empty,
                    InitialBalance    = 0,
                    Created           = DateTime.UtcNow,
                    Modified          = null
                };

    }
}
