using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookADO
{
    public class AddrBookRepo
    {
        //Connecting to SQL Server
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        //Passing the connection string as parameter
        SqlConnection sqlConnection = new SqlConnection(connectionString);
        public int RetrieveData()
        {
            //Creating a object for AddressBookModel
            AddrBookModel model = new AddrBookModel();
            //Count the number of data in table
            int count = 0;
            try
            {
                using (sqlConnection)
                {
                    //Retrieve query
                    string query = @"select * from Address_Book_Table";
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    //open sql connection
                    sqlConnection.Open();
                    //sql reader to read data
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                           DisplayDetails(sqlDataReader);
                            count++;
                        }

                    }
                    //close reader
                    sqlDataReader.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                this.sqlConnection.Close();
            }
            return count;
        }
        //Display the details
        public void DisplayDetails(SqlDataReader sqlDataReader)
        {

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
            addressBook.relationType = Convert.ToString(sqlDataReader["RelationType"]);
            Console.WriteLine("FirstName :{0} LastName :{1} Address :{2} City :{3} State :{4} ZipCode :{5} PhoneNum :{6} EmailId :{7} AddressBookName :{8} RelationType :{9} ", addressBook.firstName, addressBook.lastName, addressBook.address,addressBook.city, addressBook.stateName, addressBook.zipCode, addressBook.phoneNum, addressBook.emailId,addressBook.addrBookName,addressBook.relationType);
            Console.WriteLine("\n");
        }

    }
}

