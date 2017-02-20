using System.Net.Sockets;
using EelData.Logger;
using System.Text;
using System;
using EelData.DAL;

namespace EelData.ClientCommunicator
{
    public class TextHandler
    {
        private Model.SensorData _sensorData = new Model.SensorData();

        public void GetRequest(string text, Socket current, Model.SensorData record)
        {
            // is the text contains these characters, the sensor on the device not attached, ignore it.
            if (!text.Contains("-999"))
            {
                string id = text.Substring(0, 1);
                string seperator = text.Substring(1, 1);
                string temp = text.Substring(2, 2);
                byte tempInBytes = Byte.Parse(temp);
                int idToInt = int.Parse(id);

                record.AmbientTemperature = tempInBytes;
                record.SensorID = idToInt;
            }
            LoggerSingleton.Instance.Log("Text received: " + text);
            #region deprecated
            // client sends feed command
            //if (text.ToLower().Contains("feed"))
            //{
            //    LoggerSingleton.Instance.Log("Sending feed command to client...");
            //    byte[] data = Encoding.ASCII.GetBytes("1$");
            //    current.Send(data);
            //    LoggerSingleton.Instance.Log("Feed command sent to client");
            //}

            //else if (text.ToLower().StartsWith("ack"))
            //{
            //    LoggerSingleton.Instance.Log("Client send feed acknowledgement, the eel have been fed");
            //    //TODO - add fed event that logs to db
            //}

            ////else
            ////{
            ////    byte[] data = Encoding.ASCII.GetBytes("Invalid request");
            ////    current.Send(data);
            ////    LoggerSingleton.Instance.Log("The client sent an invalid request, warning sent");
            ////}
            #endregion deprecated
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
