using System.ServiceProcess;
using System.Timers;
using EelData.Networking;
using EelData.Logger;
using System.Diagnostics;
using EelData.DAL;
using EelData.Model;

namespace EelData
{
    public partial class MainService : ServiceBase
    {
        private Timer _dataLoggerTimer = null;
        private Model.SensorData _sensorData;

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _sensorData = new Model.SensorData();
            // create and attach the logger so we can monitor the service activity
            ILogObserver logger = new FlatFileObserver();
            LoggerSingleton.Instance.Attach(logger);
            LoggerSingleton.Instance.Log("Starting service");

            // start the socketserver and datalogger timer
            SocketServerSingleton.Instance.SetupServer();

            // start the datalogger timer 
            _dataLoggerTimer = new Timer() { Interval = 15000 };
            _dataLoggerTimer.Elapsed += DataLoggerTimer_Elapsed;
            _dataLoggerTimer.Enabled = true;

            LoggerSingleton.Instance.Log("Service started");
        }

        protected override void OnStop()
        {
            SocketServerSingleton.Instance.CloseAllSockets();
            LoggerSingleton.Instance.Log("Service stopped gracefully");
            _dataLoggerTimer.Enabled = false;
        }

        private void DataLoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DALManagerSingleton.Instance.SaveSensorData(_sensorData);
        }
    }
}
