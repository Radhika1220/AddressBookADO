using AddressBookADO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestValidation
{
    [TestClass]
    public class UnitTest1
    {
        AddrBookRepo addrBookRepo;
        ERRepoistory eRRepoistory;
        [TestInitialize]
        public void Setup()
        {
            addrBookRepo = new AddrBookRepo();
            eRRepoistory = new ERRepoistory();
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
        /// <summary>
        /// UC5-Delete the contact using name
        /// </summary>
        [TestMethod]
        public void TestMethodForDeleteContactUsingName()
        {
            int expected = 1;
            AddrBookModel addrBook = new AddrBookModel();
            var actual = addrBookRepo.DeleteParticularContact(addrBook);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC6-Retrieve Data Based on City and State
        /// </summary>
        [TestMethod]
        public void TestMethodForRetrieveDataBasedOnCityAndState()
        {
            int expected = 1;
            AddrBookModel model = new AddrBookModel();
            var actual = addrBookRepo.RetrieveDataBasedOnStateAndCity(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC7-Size Of addressBook by City and State
        /// </summary>
        [TestMethod]
        public void TestMethodForCountGroupByCityAndState()
        {
            var expected= "1 Karanataka Bangalore 1 TamilNadu Chennai 2 Maharashtra Mumbai ";
            AddrBookModel model = new AddrBookModel();
            var actual = addrBookRepo.RetrieveCountGroupByStateAndCity(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC8-Retrieve Data Sorted Alphabetically by Name for a City
        /// </summary>
        [TestMethod]
        public void TestMethodForSortAlphabeticallyByNameAndGivenCity()
        {
            int expected = 2;
            AddrBookModel model = new AddrBookModel();
            var actual = addrBookRepo.RetrieveDataBySortedAlphabetically(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// UC10-Get Count By RelationType
        /// </summary>
        [TestMethod]
        public void TestMethodForCountRelationType()
        {
            var expected = "1 Family 2 Friend 1 Profession ";
            AddrBookModel model = new AddrBookModel();
            var actual = addrBookRepo.CountRelationType(model);
            Assert.AreEqual(expected, actual);
        }
        //------------------Implementing ER Diagram Relationship------------------
        /// <summary>
        /// ----UC6--->Retrieve Person Belonging to  City or State
        /// </summary>
        [TestMethod]
        public void TestMethodForPrintDataBasedOnCityAndStateUisngERRelationship()
        {
            int expected = 3;
            AddrBookModel model = new AddrBookModel();
            int actual = eRRepoistory.PrintDataBasedOnCityAndStateUisngERRelationship(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// ----UC7--->Size of state and city
        /// </summary>
        [TestMethod]
        public void TestMethodForCountCityAndStateUsingERRelationship()
        {
            var expected = "1 Karnataka Bangalore 2 Tamil Nadu Chennai 1 Maharashtra Mumbai ";
            AddrBookModel model = new AddrBookModel();
            var actual = eRRepoistory.CountStateAndCityUsingERDiagram(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// -----UC8--->Retrieve the person data entries sorted alphabetically
        /// </summary>
        [DataTestMethod]
        public void SortedTheirNameUsingERRelationship()
        {
            int expected = 5;
            AddrBookModel model = new AddrBookModel();
            int actual = eRRepoistory.SortedtheirNameUsingERDiagram(model);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// ---	UC9--->Get Number Of contact persons by type
        /// </summary>
        [TestMethod]
        public void TestMethodForRelationTypeUsingERRelationship()
        {
            var expected = "2 Family 2 Friends 1 Profession ";
            AddrBookModel model = new AddrBookModel();
            var actual = eRRepoistory.CountTypeNameUsingERDiagram(model);
            Assert.AreEqual(expected, actual);
        }
    }
}
