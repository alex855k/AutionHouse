using System.IO;
using System.Net.Sockets;

namespace AutionHouseHost
{
    public class Client
    {
        public TcpClient client;
        public StreamWriter writer;
        public StreamReader reader;
        public string clientName;

        public Client(TcpClient client, string clientName)
        {
            this.client = client;
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            this.clientName = clientName;
        }

        public void SendMessage(string msg)
        {
            writer.WriteLine();
        }
    }
}
