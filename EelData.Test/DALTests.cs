using EelData.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EelData.Test
{
    [TestClass]
    public class DALTests
    {
        private Model.Hall _hall;
        private Model.Bassin _bassin;
        private Model.Silo _silo;
        private Model.Sensor _sensor;

        [TestInitialize]
        public void Init()
        {
            _bassin = new Model.Bassin();
            _hall = new Model.Hall();
            _silo = new Model.Silo();
            _sensor = new Model.Sensor();
            _hall.Name = "Test";
        }

        [TestMethod]
        public void SaveSensorTest()
        {
            DALManagerSingleton.Instance.SaveSensor(_sensor, 1, "192.168.100.100");
        }

        [TestMethod]
        public void SaveHallTest()
        {
            DALManagerSingleton.Instance.SaveHall(_hall);
        }

        [TestMethod]
        public void SaveBassinTest()
        {
            // to add a new bassin, enter the number of the hall you want to add the bassin to
            DALManagerSingleton.Instance.SaveBassin(_bassin, 1);
        }

        [TestMethod]
        public void SaveSiloTest()
        {
            // to add a new bassin, enter the number of the silo you want to add the silo to
            DALManagerSingleton.Instance.SaveSilo(_silo, 1);
        }
    }
}
