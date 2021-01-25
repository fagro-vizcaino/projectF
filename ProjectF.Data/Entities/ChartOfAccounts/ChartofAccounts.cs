using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Currencies;

namespace ProjectF.Data.Entities.ChartOfAccounts
{
    public class ChartofAccounts : _BaseEntity
    {
        public int AccountNumber { get; private set; }
        public Name Name { get; private set; }
        public int Parent_Id { get; private set; }
        public ChartofAccounts Parent { get; private set; }
        public ICollection<ChartofAccounts> Children { get; private set; }
        public AccountType AccountType { get; private set; }
        public Currency Currency { get; private set; }
        public AccountGroup AccountGroup { get; private set; }

        public AccountOrigin AccountOrigin { get; private set; }

        protected ChartofAccounts() { }

        public ChartofAccounts(int accountNumber
            , Name name
            , int parent_Id
            , ChartofAccounts parent
            , ICollection<ChartofAccounts> children
            , AccountType accountType
            , AccountGroup accountGroup
            , AccountOrigin accountOrigin
            , Currency currency
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            AccountNumber  = accountNumber;
            Name           = name;
            Parent_Id      = parent_Id;
            Parent         = parent;
            Children       = children;
            AccountType    = accountType;
            AccountGroup   = accountGroup;
            AccountOrigin  = accountOrigin;
            Currency       = currency;
            Created        = created == DateTime.MinValue ? DateTime.UtcNow : created;
            Status         = status;
        }


        public void EditChartofAccount(int accountNumber
            , Name name
            , int parentId
            , ChartofAccounts parent
            , ICollection<ChartofAccounts> children
            , AccountType accountType
            , AccountGroup accountGroup
            , AccountOrigin accountOrigin
            , Currency currency
            , EntityStatus status)
        {
            AccountNumber = accountNumber;
            Name          = name;
            Parent_Id     = parentId;
            Parent        = parent;
            Children      = children;
            AccountType   = accountType;
            AccountOrigin = accountOrigin;
            AccountGroup  = accountGroup;
            Currency      = currency;
            Modified      = DateTime.UtcNow;
            Status        = status;
        }
    }
}
