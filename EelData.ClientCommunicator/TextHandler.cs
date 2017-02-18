using System.Net.Sockets;
using EelData.Logger;
using System.Text;

namespace EelData.ClientCommunicator
{
    public class TextHandler
    {
        public void GetRequest(string text, Socket current)
        {
            // client sends feed command
            if (text.ToLower().Contains("feed"))
            {
                LoggerSingleton.Instance.Log("Sending feed command to client...");
                byte[] data = Encoding.ASCII.GetBytes("1$");
                current.Send(data);
                LoggerSingleton.Instance.Log("Feed command sent to client");
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
