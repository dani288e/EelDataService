using System.ServiceProcess;
using System.Timers;
using EelData.Networking;
using EelData.Logger;

namespace EelData
{
    public partial class MainService : ServiceBase
    {
        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // create and attach the logger so we can monitor the service activity
            ILogObserver logger = new FlatFileObserver();
            LoggerSingleton.Instance.Attach(logger);
            LoggerSingleton.Instance.Log("Starting service");

            // start the socketserver and datalogger timer
            SocketServerSingleton.Instance.SetupServer();

            LoggerSingleton.Instance.Log("Service started");

        }

        protected override void OnStop()
        {
            SocketServerSingleton.Instance.CloseAllSockets();
            LoggerSingleton.Instance.Log("Service stopped gracefully");
        }
    }
}
