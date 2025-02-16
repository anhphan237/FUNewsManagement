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

        public SystemAccount GetAccountById(string accountID) => _systemAccountRepository.GetAccountById(accountID);
    }
}
