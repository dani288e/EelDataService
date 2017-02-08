using System.ServiceProcess;
using System.Timers;

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
            _dataLoggerTimer.Elapsed += _dataLoggerTimer_Elapsed;
        }

        protected override void OnStop()
        {

        }

        private void _dataLoggerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

        }
    }
}
