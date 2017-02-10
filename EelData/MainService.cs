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
        AbstractLogger _loggerChain = GetChainOfLoggers();

        private static AbstractLogger GetChainOfLoggers()
        {
            AbstractLogger errorLogger = new FileLogger(AbstractLogger.Error);
            AbstractLogger fileLogger = new DebugLogger(AbstractLogger.Debug);

            errorLogger.SetNextLogger(fileLogger);
            fileLogger.SetNextLogger(errorLogger);

            return errorLogger;
        }

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
            _loggerChain.LogMessage(AbstractLogger.Debug, "Service started");

        }

        protected override void OnStop()
        {
            SocketServerSingleton.Instance.CloseAllSockets();
            _loggerChain.LogMessage(AbstractLogger.Debug, "Service stopped");
            _dataLoggerTimer.Enabled = false;
        }

        private void DataLoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO - implement database logging
        }
    }
}
