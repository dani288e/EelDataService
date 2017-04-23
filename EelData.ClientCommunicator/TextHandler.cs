using System.Net.Sockets;
using EelData.Logger;
using System.Text;
using System;
using EelData.DAL;

namespace EelData.ClientCommunicator
{
    public class TextHandler
    {
        public void GetRequest(string text, Socket current, Model.SensorData record)
        {


            // if the text contains these characters, the sensor on the device not attached, ignore it.
            if (!text.Contains("-999"))
            {
                // check if the string contains an ip address
                if (text.StartsWith(new string[] { "192", "10", "127", "172" }))
                {
                    CommunicationAgentSingleton.Instance.SendFeed(text, current);

                }

                string id = text.Substring(0, 1);
                string seperator = text.Substring(1, 1);
                string temp = text.Substring(2, 2);
                byte tempInBytes = byte.Parse(temp);
                int idToInt = int.Parse(id);

                record.AmbientTemperature = tempInBytes;
                record.SensorID = idToInt;
            }
            LoggerSingleton.Instance.Log("Text received: " + text);
            DALManagerSingleton.Instance.SaveSensorData(record);
        }
    }
}
