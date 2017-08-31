using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AutionHouseHost
{
    public class Server
    {
        private static List<Auction> _auctions = new List<Auction>();
        private static List<Client> clients = new List<Client>();
        private static TcpListener listener = null;
        private static StreamReader reader = null;
        private static StreamWriter writer = null;
        private static List<Task> clientTasks = new List<Task>();
        private static IService serviceLayer = new AuctionHouseService();

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 50000);
                listener.Start();
                BroadcastService.Clients = clients;
                Console.WriteLine("Server started");
                //_auctions.Add(new Auction("Auction1"));
                //TcpClient client = listener.AcceptTcpClient();
                var connectTask = Task.Run(() => ConnectClients());

                Task.WaitAll(connectTask);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }

        private static void ConnectClients()
        {
            Console.WriteLine("Waiting for incoming client connections...");
            while (true)
            {
                if (listener.Pending())
                {

                    clients.Add(new Client(listener.AcceptTcpClient(), "Client: " + (clients.Count + 1), serviceLayer));
                    Console.WriteLine(clients[clients.Count - 1].clientName + " client connected");
                }
            }
        }

    }
}
