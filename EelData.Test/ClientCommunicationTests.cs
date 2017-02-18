using EelData.ClientCommunicator;
using EelData.Networking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EelData.Test
{
    [TestClass]
    public class ClientCommunicationTests
    {
        [TestInitialize]
        public void Init()
        {
            SocketServerSingleton.Instance.SetupServer();
        }

        [TestMethod]
        public void GetRequestTest()
        {
            Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Loopback, 1337);

            TextHandlerSingleton.Instance.GetRequest("feed", _clientSocket);
        }
    }
}
