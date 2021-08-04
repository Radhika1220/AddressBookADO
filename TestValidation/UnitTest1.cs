using AddressBookADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestValidation
{
    [TestClass]
    public class UnitTest1
    {
        AddrBookRepo addrBookRepo;
        [TestInitialize]
        public void Setup()
        {
            addrBookRepo = new AddrBookRepo();
        }
        /// <summary>
        /// UC1-Retrieve the data using query and returns the count
        /// </summary>
        [TestMethod]
        public void TestMethodForRetriveDataUsingQuery()
        {
            int expected = 4;
            var actual = addrBookRepo.RetrieveData();
            Assert.AreEqual(expected, actual);
        }
    }
}
