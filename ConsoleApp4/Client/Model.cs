using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Client
{
    public class Model
    {
        public static UdpClient udpClient = new UdpClient(8002);

        public static List<Human> getDB()
        {
            SendMessage("getDB");
            string data = RecieveMessage();
            List<Human> humans = new List<Human>();
            string[] dataSets = data.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string elem in dataSets)
            {
                string[] attributes = elem.Split(new char[] { ',' });
                Human human = new Human(attributes[0], attributes[1], attributes[2], Convert.ToInt32(attributes[3]), Convert.ToBoolean(attributes[4]));
                humans.Add(human);
            }
            return humans;
        }

        public static void sendDB(List<Human> humans)
        {
            string output = "";
            foreach (Human elem in humans)
            {
                output += elem.lastName + "," + elem.firstName + "," + elem.fatherName + "," + Convert.ToString(elem.birthYear) + "," + Convert.ToString(elem.havePet) + ";";
            }
            SendMessage("sendDB" + output);
        }

        public static void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                udpClient.Send(data, data.Length, "127.0.0.1", 8001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static string RecieveMessage()
        {
            IPEndPoint remoteIp = (IPEndPoint)udpClient.Client.LocalEndPoint;
            try
            {
                byte[] data = udpClient.Receive(ref remoteIp);
                string message = Encoding.Unicode.GetString(data);
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}
