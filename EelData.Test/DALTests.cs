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
        private Model.SensorData _sensorData;
        private Model.Trigger _trigger;
        private Model.Warning _warning;

        [TestInitialize]
        public void Init()
        {
            _bassin = new Model.Bassin();
            _hall = new Model.Hall();
            _silo = new Model.Silo();
            _sensor = new Model.Sensor();
            _sensorData = new Model.SensorData();
            _trigger = new Model.Trigger();
            _warning = new Model.Warning();
            _hall.Name = "Test";
        }

        [TestMethod]
        public void SaveWarningTest()
        {
            DALManagerSingleton.Instance.SaveWarning(_warning);
        }

        //[TestMethod]
        //public void SaveWarningTest()
        //{
        //    DALManagerSingleton.Instance.SaveWarning(_warning, 1, 10, "high temperature in bassin");
        //}

        [TestMethod]
        public void SaveTriggerTest()
        {
            DALManagerSingleton.Instance.SaveTrigger(_trigger, 1, 2);
        }

        //[TestMethod]
        //public void SaveTempsTest()
        //{
        //    DALManagerSingleton.Instance.SaveSensorData(_sensorData);
        //}

        [TestMethod]
        public void SaveSensorTest()
        {
            DALManagerSingleton.Instance.SaveSensor(_sensor);
        }

        [TestMethod]
        public void SaveHallTest()
        {
            DALManagerSingleton.Instance.SaveHall(_hall);
        }

        [TestMethod]
        public void SaveBassinTest()
        {
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
