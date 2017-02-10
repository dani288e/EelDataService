using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using EelData.Logger;

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
        #endregion

        public void SetupServer()
        {
            LoggerSingleton.Instance.Log("Setting up server...");
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            // place the socket in a listen state. Max queue of 5 connections at a time
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(AcceptCallback, null /* insert object state here */);
            LoggerSingleton.Instance.Log("Socket server started");
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
                LoggerSingleton.Instance.Log("An object disposed exception occurred on AcceptingCallback", ex);
                return;
            }

            _clientSockets.Add(socket);
            socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, socket);
            LoggerSingleton.Instance.Log("Client connected: " + socket.RemoteEndPoint.ToString());
            _serverSocket.BeginAccept(AcceptCallback, null /* insert state stuff here */);
        }

        /// <summary>
        /// Unfinished - TODO - finish this
        /// Desktop application wants to connect to the service
        /// </summary>
        /// <param name="AR"></param>
        /// <param name="IP">IPAdress object, use this to send a command to a specific device</param>
        public void ReceiveCallback(IAsyncResult AR, IPAddress IP, string command)
        {
            Socket current = (Socket)AR.AsyncState;
            try
            {
                // try searching for the IP intered by the user
                Socket clientSocket = _clientSockets.Find(x => x.RemoteEndPoint.ToString() == IP.ToString());
                if (command != null)
                {
                    if (command.ToLower() == "feed")
                    {
                        // send data to the client device (arduino)
                        Console.WriteLine("Sending feed command to client...");
                        byte[] clientData = Encoding.ASCII.GetBytes("1$");

                        // notify the connected windows application
                        byte[] data = Encoding.ASCII.GetBytes("");
                        current.Send(data);

                        clientSocket.Send(clientData);
                        Console.WriteLine("Feed command sent to client");
                    }
                    else
                    {
                        // invalid command

                    } 
                }
            }
            catch (Exception)
            {
                // the device was not found in the list
            }

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
                LoggerSingleton.Instance.Log("Client forcefully disconnected");
                current.Close();
                _clientSockets.Remove(current);
                return;
            }

            byte[] receivedBuffer = new byte[received];
            Array.Copy(_buffer, receivedBuffer, received);
            string text = Encoding.ASCII.GetString(receivedBuffer);
            LoggerSingleton.Instance.Log("Text received: " + text);

            if (text.ToLower().Contains("feed"))
            {
                Console.WriteLine("Sending feed command to client...");
                byte[] data = Encoding.ASCII.GetBytes("1$");
                current.Send(data);
                Console.WriteLine("Feed command sent to client");
            }
            else if (text.ToLower().StartsWith("warning"))
            {
                LoggerSingleton.Instance.Log("Warning received from client");
                // TODO - add warning handling code here
            }
            else if (text.ToLower().StartsWith("ack"))
            {
                LoggerSingleton.Instance.Log("Client send feed acknowledgement, the eel have been fed");
                //TODO - add fed event that logs to db
            }
            // the text is temperature
            // TODO - Save to model and db
            else if (text.ToLower().StartsWith("1") || text.ToLower().StartsWith("2"))
            {
                //_eelClient.SensorData.Temp = text;
                //TODO - save to model
            }
            else
            {
                byte[] data = Encoding.ASCII.GetBytes("Invalid request");
                current.Send(data);
                LoggerSingleton.Instance.Log("The client sent an invalid request, warning sent");
            }
            current.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, current);
        }

        private void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            if (socket != null)
            {
                LoggerSingleton.Instance.Log("Client disconnected");
                socket.EndSend(AR);
            }
        }
    }
}
