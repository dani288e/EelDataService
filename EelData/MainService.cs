using System.ServiceProcess;
using System.Timers;
using EelData.Networking;

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
            _dataLoggerTimer = new Timer();
            _dataLoggerTimer.Interval = 150000;
            _dataLoggerTimer.Elapsed += DataLoggerTimer_Elapsed;
        }

        protected override void OnStop()
        {

        }

        private void DataLoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SocketServerSingleton.Instance.TestMethod();
        }
    }
}
