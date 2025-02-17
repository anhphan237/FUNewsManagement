using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ISystemAccountRepository
    {
        SystemAccount GetAccountByEmailAndPassword(string email, string password);
        SystemAccount GetAccountByEmail(string email);
        void UpdateSystemAccount(SystemAccount a);
        void CreateAccount(SystemAccount account);
        List<SystemAccount> GetSystemAccounts();
        int GetNumberOfAccount();
        SystemAccount GetSystemAccountById(int id);
    }
}
