using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace AutionHouseHost
{

    public class BroadcastServiceArgs : EventArgs
    {
        public Auction Auction { get; set; }
    }

    public class BroadcastService
    {
        public void OnMassMessage(object source, BroadcastServiceArgs args)
        {
            //Parallel.ForEach(args.Auction.Observers, b => b.)
        }
    }
}
