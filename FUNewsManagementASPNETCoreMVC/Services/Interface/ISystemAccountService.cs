using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISystemAccountService
    {
        SystemAccount GetAccountByEmailAndPassword(string email, string password);
        SystemAccount GetAccountByEmail(string email);
        void CreateAccount(SystemAccount account);
        void UpdateSystemAccount(SystemAccount a);
        List<SystemAccount> GetSystemAccounts();
        List<SystemAccount> GetAdminAccounts();
        int GetNumberOfAccount();
        SystemAccount GetSystemAccountById(int id);
    }
}
