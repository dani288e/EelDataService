﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using EelData.Logger;
using EelData.ClientCommunicator;
using System.Configuration;
using EelData.Model;

namespace EelData.Networking
{
    public class SocketServer
    {
        #region fields
        private readonly Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private List<Socket> _clientSockets = new List<Socket>();
        private readonly byte[] _buffer = new byte[_bufferSize];
        private const int _bufferSize = 2048;
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        private readonly IPAddress _ip = IPAddress.Parse(ConfigurationManager.AppSettings["IP"]);
        private SensorData _sensorData;
        #endregion

        public void SetupServer()
        {
            LoggerSingleton.Instance.Log("Setting up server...");

            try
            {
                _sensorData = new SensorData();
                _serverSocket.Bind(new IPEndPoint(_ip, _port));
                // place the socket in a listen state. Max queue of 5 connections at a time
                _serverSocket.Listen(5);
                _serverSocket.BeginAccept(AcceptCallback, null);
                LoggerSingleton.Instance.Log("Socket server started");
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.Log("The following error occurred on setupserver", ex);
                throw;
            }
        }

        public void CloseAllSockets()
        {
            foreach (Socket socket in _clientSockets)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (SocketException ex)
                {
                    LoggerSingleton.Instance.Log("A socketexception occurred when attempting to shutdown sockets", ex);
                    throw;
                }
                catch (ObjectDisposedException ex)
                {
                    LoggerSingleton.Instance.Log("An objectdisposedexception occurred when attempting to shutdown sockets", ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Accepts connections from clients
        /// </summary>
        /// <param name="AR"></param>
        private void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = _serverSocket.EndAccept(AR);
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.Log("An exception occurred on AcceptingCallback when attempting to EndAccept", ex);
                return;
            }

            _clientSockets.Add(socket);

            try
            {
                socket.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, socket);
                _serverSocket.BeginAccept(AcceptCallback, null /* insert state stuff here */);
                LoggerSingleton.Instance.Log("Client connected: " + socket.RemoteEndPoint.ToString());
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.Log("An exception occurred on AcceptingCallback when attempting to begingreceive/beginaccept", ex);
                throw;
            }
        }

        /// <summary>
        /// Desktop application sends request to the service
        /// </summary>
        /// <param name="AR"></param>
        /// <param name="ip">IPAdress object, use this to send a command to a specific device</param>
        public void ReceiveCallback(IAsyncResult AR, string ip, string command)
        {
            Socket current = (Socket)AR.AsyncState;
            try
            {
                // try searching for the IP entered by the user
                Socket clientSocket = _clientSockets.Find(x => x.RemoteEndPoint.ToString() == ip.ToString());
                if (command != null)
                {
                    CommunicationAgentSingleton.Instance.SendFeed(ip, current);
                }
            }
            catch (Exception ex)
            {
                LoggerSingleton.Instance.Log("An exception occurred when handling a request in receivecallback", ex);
            }

        }

        /// <summary>
        /// Handle requests from connected devices
        /// </summary>
        /// <param name="AR"></param>
        private void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (Exception)
            {
                // client disconnects by either losing the connection or closing it improperly
                LoggerSingleton.Instance.Log("Client forcefully disconnected");
                current.Close();
                _clientSockets.Remove(current);
                return;
            }

            byte[] receivedBuffer = new byte[received];
            Array.Copy(_buffer, receivedBuffer, received);
            string text = Encoding.ASCII.GetString(receivedBuffer);

            // handle the text sent from the connecting device
            TextHandlerSingleton.Instance.GetRequest(text, current, _sensorData);

            // send ack to connected device
            CommunicationAgentSingleton.Instance.SendAck(current);

            current.BeginReceive(_buffer, 0, _bufferSize, SocketFlags.None, ReceiveCallback, current);
        }

        /// <summary>
        /// Handle connections when clients disconnect properly
        /// </summary>
        /// <param name="AR"></param>
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
