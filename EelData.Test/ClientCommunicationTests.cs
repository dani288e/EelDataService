using EelData.ClientCommunicator;
using EelData.Networking;
using EelData.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Sockets;
using EelData.DAL;

namespace EelData.Test
{
    [TestClass]
    public class ClientCommunicationTests
    {
        private Model.SensorData _sensorData;

        [TestInitialize]
        public void Init()
        {
            _sensorData = new Model.SensorData();
            SocketServerSingleton.Instance.SetupServer();
        }

        [TestMethod]
        public void TempTest()
        {
            Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Loopback, 1337);

            // send a text string similar to that an arduino might send using ID 1
            TextHandlerSingleton.Instance.GetRequest("1$20.0000000000$", _clientSocket, _sensorData);

            //DALManagerSingleton.Instance.SaveSensorData(_sensorData); // din GetRequest metode, gemmer i dal'en :* så denne del behøver du ikke <3 
        }

        [TestMethod]
        public void FeedTest()
        {
            Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.Connect(IPAddress.Loopback, 1337);

            TextHandlerSingleton.Instance.GetRequest("feed", _clientSocket, _sensorData);
        }
    }
}
