using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Bid
    {
        
        public Bid(double bidValue)
        {
            this.Amount = bidValue;
        }

        public Auction Auction { get; set; }

        public double Amount { get; set; }
    }
}
