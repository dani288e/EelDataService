namespace EelData.Networking
{
    public class SocketServerSingleton
    {
        private static readonly System.Lazy<SocketServer> lazy;

        static SocketServerSingleton()
        {
            lazy = new System.Lazy<SocketServer>(() => new SocketServer());
        }

        public static SocketServer Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
