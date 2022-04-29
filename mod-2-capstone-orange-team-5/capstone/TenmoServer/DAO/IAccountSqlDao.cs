using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
   public interface IAccountDao
    {

        Account GetAccount(int accountId);


        List<Account> GetAllAccounts();

        Account Create(Account account);

        void Delete(int id);

        Account Update(int id, Account account);

    }
}
