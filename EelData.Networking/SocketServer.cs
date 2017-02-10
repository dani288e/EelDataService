using EelData.Logger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EelData.Networking
{
    public class SocketServer
    {
        #region fields
        private readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private List<Socket> _clientSockets = new List<Socket>();
        private const int _bufferSize = 2048;
        private const int _port = 1337;
        private readonly byte[] _buffer = new byte[_bufferSize];
        AbstractLogger _loggerChain = GetChainOfLoggers();
        #endregion

        private static AbstractLogger GetChainOfLoggers()
        {
            AbstractLogger infoLogger = new FileLogger(AbstractLogger.Info);
            AbstractLogger errorLogger = new FileLogger(AbstractLogger.Error);
            AbstractLogger fileLogger = new FileLogger(AbstractLogger.Debug);

            errorLogger.SetNextLogger(fileLogger);
            fileLogger.SetNextLogger(errorLogger);

            return errorLogger;
        }

        public void SetupServer()
        {
            //LoggerSingleton.Instance.WriteToLog("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            // place the socket in a listen state. This means max queue of 5 connections at a time
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(AcceptCallback, null /* insert object state here */);
            _loggerChain.LogMessage(AbstractLogger.Info, "Socket server started");
        }

        public void CloseAllSockets()
        {
            foreach (Socket socket in _clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException ex)
            {
                _loggerChain.LogMessage(3, ex.ToString());
                return;
            }

            _clientSockets.Add(socket);
            socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, socket);
            _loggerChain.LogMessage(AbstractLogger.Debug, "Client connected: " + socket.RemoteEndPoint.ToString());
            _serverSocket.BeginAccept(AcceptCallback, null /* insert state stuff here */);
        }

        /// <summary>
        /// Unfinished - TODO - finish this
        /// Desktop application wants to connect to the service
        /// </summary>
        /// <param name="AR"></param>
        /// <param name="IP">IPAdress object, use this to send a command to a specific device</param>
        public void ReceiveCallback(IAsyncResult AR, IPAddress IP)
        {

            Socket clientSocket = _clientSockets.Find(x => x.RemoteEndPoint.ToString() == IP.ToString());

        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                _loggerChain.LogMessage(AbstractLogger.Info, "Client forcefully disconnected");
                current.Close();
                _clientSockets.Remove(current);
                return;
            }

            byte[] receivedBuffer = new byte[received];
            Array.Copy(_buffer, receivedBuffer, received);
            string text = Encoding.ASCII.GetString(receivedBuffer);
            _loggerChain.LogMessage(AbstractLogger.Info, "Text received: " + text);

            if (text.ToLower().Contains("feed"))
            {
                //Socket clientSocket = _clientSockets.Find(x => x.RemoteEndPoint.ToString() == ip.ToString());
                Console.WriteLine("Sending feed command to client...");
                byte[] data = Encoding.ASCII.GetBytes("1$");
                current.Send(data);
                Console.WriteLine("Feed command sent to client");
            }
            else if (text.ToLower().StartsWith("warning"))
            {
                _loggerChain.LogMessage(AbstractLogger.Info, "Warning received from client!");
                // TODO - add warning handling code here
            }
            else if (text.ToLower().StartsWith("ack"))
            {
                _loggerChain.LogMessage(AbstractLogger.Info, "Client sent feed acknowledgement, the eel have been fed");
                //TODO - add fed event that logs to db
            }
            // the text is temperature
            // TODO - Save to model and db
            else if (text.ToLower().StartsWith("1") || text.ToLower().StartsWith("2"))
            {
                //_eelClient.SensorData.Temp = text;
            }
            else
            {
                _loggerChain.LogMessage(AbstractLogger.Info,"Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                _loggerChain.LogMessage(AbstractLogger.Info, "Warning Sent");
            }
            current.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, current);
        }

        private void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            if (socket != null)
            {
                _loggerChain.LogMessage(AbstractLogger.Info, "Client disconnected...");
                socket.EndSend(AR);
            }
        }
    }
}
