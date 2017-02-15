using EelData.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EelData.Test
{
    [TestClass]
    public class DALTests
    {
        private Model.Hall _hall;

        [TestInitialize]
        public void Init()
        {
            _hall = new Model.Hall();
            _hall.Name = "Test";
        }

        [TestMethod]
        public void SaveHallTest()
        {
            DALManagerSingleton.Instance.SaveHall(_hall);
        }

        [TestMethod]
        public void SaveBassinTest()
        {
            DALManagerSingleton.Instance.SaveBassin();
        }
    }
}
