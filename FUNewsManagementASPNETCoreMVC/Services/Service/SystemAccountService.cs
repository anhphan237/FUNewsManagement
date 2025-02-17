using BusinessObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _systemAccountRepository;

        public SystemAccountService()
        {
            _systemAccountRepository = new SystemAccountRepository();   
        }

        public void CreateAccount(SystemAccount account)
            => _systemAccountRepository.CreateAccount(account);

        public SystemAccount GetAccountByEmail(string email)
            => _systemAccountRepository.GetAccountByEmail(email);

        public SystemAccount GetAccountByEmailAndPassword(string email, string password) 
            => _systemAccountRepository.GetAccountByEmailAndPassword(email, password);

        public int GetNumberOfAccount()
            => _systemAccountRepository.GetNumberOfAccount();

        public SystemAccount GetSystemAccountById(int id)
            => _systemAccountRepository.GetSystemAccountById(id);

        public List<SystemAccount> GetSystemAccounts()
            => _systemAccountRepository.GetSystemAccounts();    

        public void UpdateSystemAccount(SystemAccount a)
            => _systemAccountRepository.UpdateSystemAccount(a);
    }
}
