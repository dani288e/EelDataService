using EelData.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EelData.ClientCommunicator
{
    public class CommunicationAgent
    {
        public void SendFeed(string ip, Socket current)
        {
            byte[] data = Encoding.ASCII.GetBytes("feed");
            current.Send(data);

            LoggerSingleton.Instance.Log("Sending feed command to IP: " + ip);
        }

        public void SendAck(Socket current)
        {
            LoggerSingleton.Instance.Log("Sending ack to client...");
            byte[] data = Encoding.ASCII.GetBytes("ack");
            current.Send(data);
            LoggerSingleton.Instance.Log("ack sent to client");
        }
    }
}
