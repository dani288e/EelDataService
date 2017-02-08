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
        private bool _shouldFeed = true;
        #endregion

        public void SetupServer()
        {
            LoggerSingleton.Instance.WriteToLog("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            // place the socket in a listen state. This means max queue of 5 connections at a time
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(AcceptCallback, null /* insert object state here */);
            LoggerSingleton.Instance.WriteToLog("Server setup complete");
        }

        public void CloseAllSockets()
        {
            foreach (Socket socket in _clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        public void TestMethod()
        {
            LoggerSingleton.Instance.WriteToLog("Test method fired");
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
                LoggerSingleton.Instance.WriteToLog(ex.ToString());
                return;
            }

            _clientSockets.Add(socket);
            socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, socket);
            LoggerSingleton.Instance.WriteToLog("Client connected: " + socket.RemoteEndPoint.ToString());
            _serverSocket.BeginAccept(AcceptCallback, null /* insert state stuff here */);
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
                LoggerSingleton.Instance.WriteToLog("Client forcefully disconnected");
                current.Close();
                _clientSockets.Remove(current);
                return;
            }

            byte[] receivedBuffer = new byte[received];
            Array.Copy(_buffer, receivedBuffer, received);
            string text = Encoding.ASCII.GetString(receivedBuffer);
            LoggerSingleton.Instance.WriteToLog("Text received: " + text);

            if (text.ToLower() == "feed" || _shouldFeed == true)
            {
                LoggerSingleton.Instance.WriteToLog("Sending feed command to client...");
                byte[] data = Encoding.ASCII.GetBytes("1$");
                current.Send(data);
                LoggerSingleton.Instance.WriteToLog("Feed command sent to client");
                _shouldFeed = false;
            }
            else if (text.ToLower().StartsWith("warning"))
            {
                LoggerSingleton.Instance.WriteToLog("Warning received from client!");
                // TODO - add warning handling code here
            }
            else if (text.ToLower().StartsWith("ack"))
            {
                LoggerSingleton.Instance.WriteToLog("Client sent feed acknowledgement, the eel have been fed");
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
                LoggerSingleton.Instance.WriteToLog("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                LoggerSingleton.Instance.WriteToLog("Warning Sent");
            }
            current.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, current);
        }

        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            if (socket != null)
            {
                socket.EndSend(AR);
                LoggerSingleton.Instance.WriteToLog("Client disconnected...");
            }
        }
    }
}
