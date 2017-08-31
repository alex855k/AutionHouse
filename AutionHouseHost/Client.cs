using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

namespace AutionHouseHost
{
    public class Client
    {
        public TcpClient client;
        public StreamWriter writer;
        public StreamReader reader;
        public string clientName;
        private Thread receiver;
        private Thread broadcaster;
        private IService _serviceLayer;

        public Client(TcpClient client, string clientName, IService serviceLayer)
        {
            _serviceLayer = serviceLayer;
            this.client = client;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;
            this.clientName = clientName;
            receiver = new Thread(RunReceiver);
            receiver.Start();

        }

        private void RunReceiver()
        {
            while (true)
            {
                try
                {
                    string stringRequest = reader.ReadLine();
                    if (!_serviceLayer.Command(stringRequest.ToLower(), this))
                    {
                        writer.Write("Invalid command you idiot");
                    }

                }
                catch (Exception ex)
                {
                    if (!(ex is ThreadAbortException))
                    {
                        Console.WriteLine(" >> " + ex.ToString());
                        client.Close();
                        Thread.CurrentThread.Abort();
                    }
                }

            }
        }

        private void RunBroadcaster()
        {


        }

        public void SendMessage(string msg)
        {
            writer.WriteLine(msg);
        }
    }

    public class AuctionHouseService : IService
    {
        public AuctionHouseService()
        {

        }

        public bool Command(string cmd, Client c)
        {
            bool cond = false;
            string[] stringArr = cmd.Split(' ');
            switch (stringArr[0])
            {
                case "bid":
                    double bidAmount;
                    if (stringArr.Count() > 1)
                    {
                        if (double.TryParse(stringArr[1], out bidAmount))
                        {
                            c.SendMessage("Bid was placed: " + bidAmount);

                            BroadcastService.SendAll("message to all");
                        }
                    }
                    break;
            }
            return cond;
        }

    }

    public interface IService
    {
        bool Command(string cmd, Client c);
    }

}
