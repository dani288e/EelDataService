namespace EelData.Logger
{
    public abstract class AbstractLogger
    {
        public static int Info = 1;
        public static int Debug = 2;
        public static int Error = 3;

        protected int level;

        // next element in chain of responsobility
        protected AbstractLogger nextLogger;

        public void SetNextLogger(AbstractLogger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public void LogMessage(int level, string message)
        {
            if (this.level <= level)
            {
                Write(message);
            }

            if (nextLogger != null)
            {
                nextLogger.LogMessage(level, message);
            }
        }
        abstract protected void Write(string message);
    }
}
