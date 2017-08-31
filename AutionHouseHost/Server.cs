using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassLibrary1;

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

        static void Main(string[] args)
        {
            try
            {
                listener = new TcpListener(50000);
                listener.Start();
                Console.WriteLine("Server started");
                _auctions.Add(new Auction("Auction1"));
                _auctions.Add(new Auction("Auction2"));
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
                    
                    clients.Add(new Client(listener.AcceptTcpClient(), "Client: " + (clients.Count + 1)));
                    Console.WriteLine(clients[clients.Count - 1].clientName + " client connected");
                    clientTasks.Add(Task.Run(() => HandleClient(clients[clients.Count - 1])));
                }
            }
        }

        private static void HandleClient(Client TCPClient)
        {
            StreamReader sr = new StreamReader(TCPClient.client.GetStream());
            StreamWriter sw = new StreamWriter(TCPClient.client.GetStream());
            sw.AutoFlush = true;

            while (true)
            {
                try
                {
                    string stringRequest = sr.ReadLine();
                    string[] strArr = stringRequest.Split(' ');
                    double bidAmount;
                    if (strArr[0].ToLower() == "bid" && double.TryParse(strArr[1], out bidAmount))
                    {
                        sw.WriteLine("Bid was placed: " + bidAmount);
                        sw.Flush();
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is ThreadAbortException))
                    {
                        Console.WriteLine(" >> " + ex.ToString());
                        TCPClient.client.Close();
                        Thread.CurrentThread.Abort();
                    }
                }

            }
        }
    }
}
