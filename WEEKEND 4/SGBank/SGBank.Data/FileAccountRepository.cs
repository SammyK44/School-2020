using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        private string path = "Accounts.txt";
        private List<Account> list = new List<Account>();
        private string AccountToString(Account account)
        {
            char type;
            switch (account.Type)
            {
                case AccountType.Free:
                    type = 'F';
                    break;
                case AccountType.Basic:
                    type = 'B';
                    break;
                case AccountType.Premium:
                    type = 'P';
                    break;
                default:
                    throw new Exception("CRITICAL ERROR: Account type is nonexistant. Contact IT");
            }
            return $"{account.AccountNumber},{account.Name},{account.Balance},{type}";
        }
        private void WriteAccounts()
        {
            StreamWriter writer = new StreamWriter(path);
            foreach (Account account in list)
            {
                writer.WriteLine(AccountToString(account));
            }
            writer.Close();
        }
        private void ReadAccounts()
        {
            list.Clear();
            foreach (string line in File.ReadAllLines(path))
            {
                string[] set = line.Split(new[] { ',' });

                AccountType type = new AccountType();
                switch (set[3])
                {
                    case "F":
                        type = AccountType.Free;
                        break;
                    case "B":
                        type = AccountType.Basic;
                        break;
                    case "P":
                        type = AccountType.Premium;
                        break;
                    default:
                        throw new Exception("CRITICAL ERROR: Account type written into file was invalid. Contact IT");
                }
                
                list.Add(new Account { AccountNumber = set[0], Name = set[1] , Balance = decimal.Parse(set[2]), Type = type});
            }
        }

        public Account LoadAccount(string AccountNumber)
        {
            ReadAccounts();
            foreach (Account account in list)
            {
                if (AccountNumber == account.AccountNumber)
                {
                    return account;
                }
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            foreach (Account listAccount in list)
            {
                if (account.AccountNumber == listAccount.AccountNumber)
                {
                    list.Remove(listAccount);
                    list.Add(account);
                    WriteAccounts();
                    return;
                }
            }
            throw new Exception("CRITICAL ERROR: Account could not be saved because it wasn't on the list. Contact IT");
        }
    }
}
