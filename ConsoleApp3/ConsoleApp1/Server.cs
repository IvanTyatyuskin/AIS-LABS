using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using NLog;


namespace ConsoleApp1
{
    class Server
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private UdpClient udpClient_S;
        private int port;
        View view = new View();
        Logger logger = LogManager.GetCurrentClassLogger();

        public Server(int _port)
        {
            udpClient_S = new UdpClient(_port);
            this.port = _port;
            logger.Info("Сервер работает");
        }

        public void StartListenAsync()
        {
            while (true)
            {
                allDone.Reset();
                udpClient_S.BeginReceive(RequestCallback, udpClient_S);
                allDone.WaitOne();
            }
        }

        private void RequestCallback(IAsyncResult ar)
        {
            allDone.Set();
            var listener = (UdpClient)ar.AsyncState;
            var ep = (IPEndPoint)udpClient_S.Client.LocalEndPoint;
            var res = listener.EndReceive(ar, ref ep);
            string data = Encoding.Unicode.GetString(res);
            logger.Info("Сообщение от клиента: {0}", data);
            byte[] z = Encoding.Unicode.GetBytes(view.ViewReply(data));
            udpClient_S.SendAsync(z, z.Length, ep);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(8001);
            server.StartListenAsync();
        }


    }
}
