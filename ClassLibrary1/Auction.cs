using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Auction
    {
        private readonly double _minimumBid = 100;
        private double _highestBidValue = 0;
        public int AuctionTime { get; } = 10;
        public List<Bid> _bids = new List<Bid>();
        public string AuctionName { get; }
        private static int auctionId = 0;
        public int AuctionId { get; }
        public string AuctionDescription { get; }
        public Item Item { get; set; }
        private Thread AuctionThread;

        public Auction(string name)
        {
            AuctionId = auctionId++;
            AuctionName = name;
            AuctionThread = new Thread(Run);
        }

        private void Run()
        {
            StartTimer();
            while (true)
            {
                Console.WriteLine("auction number" + auctionId + " " + AuctionName);
                Thread.Sleep(1000);
            }
        }

        private void StartTimer()
        {
           Console.WriteLine("Auction has begun");
        }

        public Bid HighestBid
        {
            get
            {
                return _bids.Find(bt => bt.Amount == _bids.Max(b => b.Amount));
                
            }
        }

        public List<IObse> Observers { get; set; }

        public Auction(Item item, string auctionDescription, double minimumBid)
        {
            Item = item;
            AuctionDescription = auctionDescription;
            _minimumBid = minimumBid;
            AuctionName = "Auction for " + item.Name + "#"+ item.ID;
        }

        public Auction(Item item, string auctionDescription, double minimumBid, int auctionTime, string auctionName)
        {
            Item = item;
            AuctionDescription = auctionDescription;
            _minimumBid = minimumBid;
            AuctionTime = auctionTime;
            AuctionName = auctionName;
        }

        public bool CanMakeBid(double bidValue)
        {
            bool canMakeBid = false;
            if(bidValue > _minimumBid)
            {
                Bid highest = HighestBid;
                if (highest != null)
                {
                    if (highest.Amount < bidValue)
                    {   
                        canMakeBid = true;
                    }   
                }
                else
                {
                      canMakeBid = true;
                }
            }
            return canMakeBid;
        }

        public void SubmitBit(double bidValue, BidderAccount bidder)
        {
            Bid b = new Bid(bidValue);
            _bids.Add(b);
            bidder.AddBid(b);
        }

        public void EndAuction()
        {

        }

    }
}
