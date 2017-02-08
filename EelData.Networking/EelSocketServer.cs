using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EelData.Networking
{
    public class EelSocketServer : SocketServerSingleton
    {
        #region fields
        private readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private List<Socket> _clientSockets = new List<Socket>();
        private const int _bufferSize = 2048;
        private const int _port = 1337;
        private readonly byte[] _buffer = new byte[_bufferSize];
        private bool _shouldFeed = true;

        #endregion

        public void TestMethod()
        {

        }
    }
}
