﻿using BusinessObjects;
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
        public SystemAccount GetAccountById(string accountID) => SystemAccountDAO.GetAccountById(accountID);
    }
}
