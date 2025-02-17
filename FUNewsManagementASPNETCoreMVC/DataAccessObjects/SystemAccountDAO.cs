﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class SystemAccountDAO
    {
        public static SystemAccount GetAccountByEmailAndPassword(string email, string password)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts
                .FirstOrDefault(a => a.AccountEmail == email && a.AccountPassword == password);
        }

        public static SystemAccount GetAccountByEmail(string email)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts
                .FirstOrDefault(a => a.AccountEmail == email);
        }

        public static void CreateAccount(SystemAccount account)
        {
            using var db = new FUNewsManagementContext();
            db.SystemAccounts.Add(account);
            db.SaveChanges();
        }

        public static int GetNumberOfAccount()
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.Count();   
        }
    }
}
