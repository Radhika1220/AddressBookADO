using System;
using System.Collections.Generic;

namespace AddressBookADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******Address Book System Using ADO***********");
            List<AddrBookModel> list = new List<AddrBookModel>();
            AddrBookRepo addrBookRepo = new AddrBookRepo();
            ERRepoistory eRRepoistory = new ERRepoistory();
            AddrBookModel addrBook = new AddrBookModel();
            Transcation transcation = new Transcation();
            Console.WriteLine("1.Connecting to DB And Retrieve the data from sql server");
            Console.WriteLine("2.Insert into table");
            Console.WriteLine("3.Edit the existing contact using update query");
            Console.WriteLine("4.Delete the Contact");
            Console.WriteLine("5.Retrieve Data Based on City And State");
            Console.WriteLine("6.Retrieve Count Group By State And City");
            Console.WriteLine("7.Sort the name alphabetically and based on city also");
            Console.WriteLine("8.Count the RealtionType");
            Console.WriteLine("************Implementing ER Diagram RelationShip");
            Console.WriteLine("9.Retrieve the data based on state and city");
            Console.WriteLine("10.Count of State And City");
            Console.WriteLine("11.Count the relationtype");
            Console.WriteLine("12.Sorted Alphabetically by name");
            Console.WriteLine("13.Adding a date field column and update the values");
            Console.WriteLine("14.Retrieve Details based on Date Range");
            Console.WriteLine("15.Insert into tables using transcation");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                       addrBookRepo.RetrieveData();
                    break;
                case 2:
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
                    addrBookRepo.InsertIntoTable(addrBook);
                    break;
                case 3:
                    addrBookRepo.EditExistingContact(addrBook);
                    break;
                case 4:
                    addrBookRepo.DeleteParticularContact(addrBook);
                    break;
                case 5:
                    addrBookRepo.RetrieveDataBasedOnStateAndCity(addrBook);
                    break;
                case 6:
                    addrBookRepo.RetrieveCountGroupByStateAndCity(addrBook);
                    break;
                case 7:
                    addrBookRepo.RetrieveDataBySortedAlphabetically(addrBook);
                    break;
                case 8:
                    addrBookRepo.CountRelationType(addrBook);
                    break;
                case 9:
                    eRRepoistory.PrintDataBasedOnCityAndStateUisngERRelationship(addrBook);
                    break;
                case 10:
                    eRRepoistory.CountStateAndCityUsingERDiagram(addrBook);
                    break;
                case 11:
                    eRRepoistory.CountTypeNameUsingERDiagram(addrBook);
                    break;
                case 12:
                    eRRepoistory.SortedtheirNameUsingERDiagram(addrBook);
                    break;
                case 13:
                    //transcation.AlterTableaddStartDate();
         
                    transcation.UpdateStartDateValueBasedOnContctId();
                    break;
                case 14:
                    transcation.RetrieveDataBasedOnDateRange();
                    break;
                case 15:
                    transcation.InsertIntoTablesUsingTranscation();
                    break;
              
                case 16:
                    transcation.ImplementingUsingThread();
                    break;
            }
        }
    }
}
