using BusinessObjects;
using DataAccessObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public void CreateAccount(SystemAccount account)
            => SystemAccountDAO.CreateAccount(account); 
        public SystemAccount GetAccountByEmail(string email)
            => SystemAccountDAO.GetAccountByEmail(email);
        public SystemAccount GetAccountByEmailAndPassword(string email, string password) 
            => SystemAccountDAO.GetAccountByEmailAndPassword(email,password);
        public int GetNumberOfAccount()
            => SystemAccountDAO.GetNumberOfAccount();
        public SystemAccount GetSystemAccountById(int id)
            => SystemAccountDAO.GetSystemAccountById(id);
        public List<SystemAccount> GetSystemAccounts()
            => SystemAccountDAO.GetSystemAccounts();    

        public void UpdateSystemAccount(SystemAccount a)
            => SystemAccountDAO.UpdateSystemAccount(a);
    }
}
