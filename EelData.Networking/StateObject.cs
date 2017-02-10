namespace EelData.Networking
{
    public class StateObject
    {
        // client socket
        public SocketServer workSocket = null;
        // size of the receive buffer
        public const int BufferSize = 1024;
    }
}
