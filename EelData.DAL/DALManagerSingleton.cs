namespace EelData.DAL
{
    public class DALManagerSingleton
    {
        private static readonly System.Lazy<DALManager> lazy;

        static DALManagerSingleton()
        {
            lazy = new System.Lazy<DALManager>(() => new DALManager());
        }

        public static DALManager Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
