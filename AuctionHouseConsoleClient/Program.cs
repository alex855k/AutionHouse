using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseConsoleClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("localhost", 50000);
            StreamReader sr = new StreamReader(client.GetStream());
            StreamWriter sw = new StreamWriter(client.GetStream());

            try
            {
                string data = Console.ReadLine();
                sw.WriteLine(data);
                sw.Flush();
                while (data != null)
                {
                    data = sr.ReadLine();
                    Console.WriteLine(data);

                    data = Console.ReadLine();
                    sw.WriteLine(data);
                    sw.Flush();
                }
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
