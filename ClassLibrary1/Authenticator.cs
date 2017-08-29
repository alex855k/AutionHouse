using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Authenticator
    {
        private Dictionary<string,BidderAccount>_accounts = new Dictionary<string, BidderAccount>();
        public Authenticator(List<BidderAccount> accounts)
        {
            foreach (var a in accounts)
            {
                _accounts.Add(a.AccountName,a);
            }
        }

        public bool Login(string accountName, string password)
        {
            if (_accounts.ContainsKey(accountName))
            {
                BidderAccount account = _accounts[accountName];
                if (account.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
