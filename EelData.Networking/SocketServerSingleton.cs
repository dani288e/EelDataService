namespace EelData.Networking
{
    public class SocketServerSingleton
    {
        private static readonly System.Lazy<EelSocketServer> lazy;

        static SocketServerSingleton()
        {
            lazy = new System.Lazy<EelSocketServer>(() => new EelSocketServer());
        }

        public static EelSocketServer Instance { get { return lazy.Value; } }
    }
}
