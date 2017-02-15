﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using EelData.Networking;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace EelData.Test
{
    [TestClass]
    public class SocketServerTests
    {
        public SocketServerTests()
        {
            SocketServerSingleton.Instance.SetupServer();
        }

        [TestMethod]
        public void MiniClientTest()
        {
            Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Loopback, 1337);
            string request = "Test string sent";
            byte[] buffer = Encoding.ASCII.GetBytes(request);
            _clientSocket.Send(buffer);
        }
    }
}
