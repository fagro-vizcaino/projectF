using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF.Data.Entities.ChartOfAccounts
{
    public enum AccountType
    {
        [Display(Name = "Control")]
        Control,
        [Display(Name = "Auxiliar")]
        Auxiliary
    }

    public enum AccountOrigin
    {
        [Display(Name ="Deudor")]
        Debtor, 
        [Display(Name = "Acredor")]
        Creditor
    }

    public enum AccountGroup 
    {
        [Display(Name = "Activo")]
        Asset,
        [Display(Name = "Pasivo")]
        Liabilities,
        Capital,
        [Display(Name = "Ingreso")]
        Income,
        [Display(Name = "Costo")]
        Cost,
        [Display(Name = "Gasto")]
        Expenses
    }
}
