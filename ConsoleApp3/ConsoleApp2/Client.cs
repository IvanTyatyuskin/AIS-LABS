using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApp2
{
    class Client
    {
        public static UdpClient udpClient;
        static void Main(string[] args)
        {
            udpClient = new UdpClient(8002);
            Console.WriteLine("Клиент работает, введите Enter чтобы начать");
            while(true)
            {
                try
                {
                    SendMessage();
                    RecieveMessage();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void SendMessage()
        {
            try
            {
                string message = Convert.ToString(Console.ReadLine());
                byte[] data = Encoding.Unicode.GetBytes(message);
                udpClient.Send(data, data.Length, "127.0.0.1", 8001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void RecieveMessage()
        {
            IPEndPoint remoteIp = (IPEndPoint)udpClient.Client.LocalEndPoint;
            try
            {
                byte[] data = udpClient.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
                Console.Clear();
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
