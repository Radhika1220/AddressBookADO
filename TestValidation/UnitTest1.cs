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
        /// UC1&UC2-Retrieve the data using query and returns the count
        /// </summary>
        [TestMethod]
        public void TestMethodForRetriveDataUsingQuery()
        {
            int expected = 5;
            var actual = addrBookRepo.RetrieveData();
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC3-Insert the values in address_book_table
        /// </summary>
        [TestMethod]
        public void TestMethodInsertIntoTable()
        {
            int expected = 1;
            AddrBookModel addrBook = new AddrBookModel();
            addrBook.firstName = "Radhika";
            addrBook.lastName = "Shankar";
            addrBook.address = "Adam Street";
            addrBook.city = "Chennai";
            addrBook.stateName = "Tamilnadu";
            addrBook.zipCode = "600013";
            addrBook.phoneNum = 8974587457;
            addrBook.emailId = "radz@gmail.com";
            addrBook.addrBookName = "Cousin";
            addrBook.relationType = "Family";
            var actual = addrBookRepo.InsertIntoTable(addrBook);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC4-Edit the existing contact using update query(by their name)
        /// </summary>
        [TestMethod]
        public void TestMethodForUpdateQuery()
        {
            int expected = 1;
            AddrBookModel addrBook = new AddrBookModel();
            var actual = addrBookRepo.EditExistingContact(addrBook);
            Assert.AreEqual(expected, actual);
        }
    }
}
