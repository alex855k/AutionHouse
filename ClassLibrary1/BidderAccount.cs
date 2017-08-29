using System.Collections.Generic;

namespace ClassLibrary1
{
    public class BidderAccount
    {
        public double Balance { get; private set; } = 10000;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }

        private List<Bid> _bids = new List<Bid>();

        public BidderAccount(string firstName, string lastName, string accountName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountName = accountName;
            Password = password;
        }

        public void AddBid(Bid b)
        {
            _bids.Add(b);
        }
    }
}