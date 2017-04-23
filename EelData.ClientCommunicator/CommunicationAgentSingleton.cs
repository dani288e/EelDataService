namespace EelData.ClientCommunicator
{
    public class CommunicationAgentSingleton
    {
        private static readonly System.Lazy<CommunicationAgent> lazy;

        static CommunicationAgentSingleton()
        {
            lazy = new System.Lazy<CommunicationAgent>(() => new CommunicationAgent());
        }

        public static CommunicationAgent Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
