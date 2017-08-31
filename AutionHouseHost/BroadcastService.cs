using ClassLibrary1;
using System;
using System.Collections.Generic;

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

        public static void SendAll(string messageToAll)
        {
            foreach (Client c in Clients)
            {
                c.SendMessage(messageToAll);
            }
        }

        public static List<Client> Clients { get; set; }
    }
}
