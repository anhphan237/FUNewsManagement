using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class SystemAccountDAO
    {
        public static SystemAccount GetAccountById(string accountID)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.FirstOrDefault(c => c.AccountId.Equals(accountID));
        }
    }
}
