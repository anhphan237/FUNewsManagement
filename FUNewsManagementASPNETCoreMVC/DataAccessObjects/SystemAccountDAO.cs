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
        public static List<SystemAccount> GetSystemAccounts()
        {
            var systemAccounts = new List<SystemAccount>();
            try
            {
                using var db = new FUNewsManagementContext();
                systemAccounts = db.SystemAccounts.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return systemAccounts;
        }

        public static List<SystemAccount> GetAdminAccounts()
        {
            var systemAccounts = new List<SystemAccount>();
            try
            {
                using var db = new FUNewsManagementContext();
                systemAccounts = db.SystemAccounts
                    .Where(a => a.AccountRole == 1)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return systemAccounts;
        }

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

        public static void UpdateSystemAccount(SystemAccount a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Entry<SystemAccount>(a).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static SystemAccount GetSystemAccountById(int id)
        {
            using var db = new FUNewsManagementContext();
            return db.SystemAccounts.FirstOrDefault(c => c.AccountId == id);
        }
    }
}
