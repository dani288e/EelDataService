using System.ServiceProcess;
using System.Timers;
using EelData.Networking;
using EelData.Logger;
using System.Diagnostics;

namespace EelData
{
    public partial class MainService : ServiceBase
    {
        private Timer _dataLoggerTimer = null;


        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Debugger.Launch();
            // start the socketserver
            SocketServerSingleton.Instance.SetupServer();


            _dataLoggerTimer = new Timer() { Interval = 150000 };
            _dataLoggerTimer.Elapsed += DataLoggerTimer_Elapsed;

            LoggerSingleton.Instance.Log("Service started");
        }

        protected override void OnStop()
        {
            SocketServerSingleton.Instance.CloseAllSockets();
            LoggerSingleton.Instance.Log("Service stopped");
            _dataLoggerTimer.Enabled = false;
        }

        private void DataLoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO - implement database logging
        }
    }
}
