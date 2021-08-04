using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookADO
{
    
    public class ERRepoistory
    {
        //Connecting to SQL Server
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        //Passing the connection string as parameter
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        /// <summary>
        /// ----UC6--->Retrieve Person Belonging to  City or State
        /// </summary>
        /// <param name="addrBookModel"></param>
        /// <returns></returns>
        public int PrintDataBasedOnCityAndStateUisngERRelationship(AddrBookModel addrBookModel)
        {
            int count = 0;
            AddrBookModel addressBook = new AddrBookModel();
            //query to be executed
            string query = @"select AddressBookName, FirstName, LastName, Address, City, StateName, ZipCode, PhoneNum, EmailId, ContactTypeName
from Contact_Person
INNER JOIN  AddressBook on AddressBook.AddressBookID = Contact_Person.AddressBook_ID and (City='Chennai' or StateName='TamilNadu')
INNER JOIN Relation_Type on Relation_Type.Contact_ID = Contact_Person.ContactID
INNER JOIN Contact_Type on Relation_Type.ContactType_ID = Contact_Type.ContactTypeID";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayDetails(sqlDataReader);
                    count++;
                    //contacts.Add(addressBook);
                }
            }
            // return contacts.Count;
            return count;
        }
        /// <summary>
        /// ----UC7--->Size of state and city
        /// </summary>
        /// <param name="addrBookModel"></param>
        /// <returns></returns>
        public string CountStateAndCityUsingERDiagram(AddrBookModel addrBookModel)
        {
            string list = null;
            AddrBookModel addressBook = new AddrBookModel();
            //query to be executed
            string query = @"select Count(*)As CountOfStateAndCity, StateName, City
           from Contact_Person
           INNER JOIN  AddressBook on AddressBook.AddressBookID = AddressBook_ID
             Group by StateName,City";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("Count: {0} StateName : {1} CityName : {2} ", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                    list += sqlDataReader[0] + " " + sqlDataReader[1] + " " + sqlDataReader[2] + " ";
                    Console.WriteLine(list);             
                }
            }
            return list;
        }
        public string CountTypeNameUsingERDiagram(AddrBookModel addrBookModel)
        {
            string list = null;
            AddrBookModel addressBook = new AddrBookModel();
            //query to be executed
            string query = @"select Count(*) as NumberOfContacts,Contact_Type.ContactTypeName
from Contact_Person 
INNER JOIN  AddressBook on AddressBook.AddressBookID=Contact_Person.AddressBook_ID
INNER JOIN Relation_Type on Relation_Type.Contact_ID=Contact_Person.ContactID
INNER JOIN Contact_Type on Relation_Type.ContactType_ID=Contact_Type.ContactTypeID
Group by ContactTypeName";
           ;
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("NumOfContacts: {0} ContactType : {1} ", sqlDataReader[0], sqlDataReader[1]);
                    list += sqlDataReader[0] + " " + sqlDataReader[1] + " ";
                    Console.WriteLine(list);
                }
            }
            return list;
        }
        /// <summary>
        /// -----UC8--->Retrieve the person data entries sorted alphabetically
        /// </summary>
        /// <param name="addrBookModel"></param>
        /// <returns></returns>
        public int SortedtheirNameUsingERDiagram(AddrBookModel addrBookModel)
        {
            int count = 0;
            AddrBookModel addressBook = new AddrBookModel();
            //query to be executed
            string query = @"select AddressBookName,FirstName,LastName,Address,City,StateName,ZipCode,PhoneNum,EmailId,ContactTypeName
           from Contact_Person 
            INNER JOIN  AddressBook on AddressBook.AddressBookID=AddressBook_ID 
         INNER JOIN Relation_Type on Relation_Type.Contact_Id=Contact_Person.ContactID
       INNER JOIN Contact_Type on Relation_Type.ContactType_ID=Contact_Type.ContactTypeID
        order by(FirstName)";
            SqlCommand sqlCommand = new SqlCommand(query, this.sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    DisplayDetails(sqlDataReader);
                    count++;
                }
            }
            return count;
        }
        public void DisplayDetails(SqlDataReader sqlDataReader)
        {
            //Creating object for addressbook model
            AddrBookModel addressBook = new AddrBookModel();

            addressBook.firstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.lastName = Convert.ToString(sqlDataReader["LastName"]);
            addressBook.address = Convert.ToString(sqlDataReader["Address"]);
            addressBook.city = Convert.ToString(sqlDataReader["City"]);
            addressBook.stateName = Convert.ToString(sqlDataReader["StateName"]);
            addressBook.zipCode = Convert.ToString(sqlDataReader["ZipCode"]);
            addressBook.phoneNum = Convert.ToDouble(sqlDataReader["Phonenum"]);
            addressBook.emailId = Convert.ToString(sqlDataReader["EmailId"]);
            addressBook.addrBookName = Convert.ToString(sqlDataReader["AddressBookName"]);

            addressBook.contactTypeName = Convert.ToString(sqlDataReader["ContactTypeName"]);
            Console.WriteLine("FirstName :{0} LastName :{1} Address :{2} City :{3} State :{4} ZipCode :{5} PhoneNum :{6} EmailId :{7} AddressBookName :{8} ContactTypeName :{9} ", addressBook.firstName, addressBook.lastName, addressBook.address, addressBook.city, addressBook.stateName, addressBook.zipCode, addressBook.phoneNum, addressBook.emailId, addressBook.addrBookName, addressBook.contactTypeName);
            Console.WriteLine("\n");
        }
    }
}
