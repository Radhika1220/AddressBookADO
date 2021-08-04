using System;

namespace AddressBookADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******Address Book System Using ADO***********");

            AddrBookRepo addrBookRepo = new AddrBookRepo();
            AddrBookModel addrBook = new AddrBookModel();
            Console.WriteLine("1.Connecting to DB And Retrieve the data from sql server");
            Console.WriteLine("2.Insert into table");
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
        }
        }
    }
}
