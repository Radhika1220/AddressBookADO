using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddressBookADO
{/// <summary>
/// ------------------------------Transcation Related Opeartions---------------------------------------------------
/// </summary>
    public class Transcation
    {
        //Give path for Database Connection
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        //Represents a connection to Sql Server Database
        SqlConnection SqlConnection = new SqlConnection(connection);
        List<AddrBookModel> addrList = new List<AddrBookModel>();
        /// <summary>
        /// Adding a Date Column in Contact_Person
        /// </summary>
        /// <returns></returns>
        public int AlterTableAddStartDateValue()
        {
            int result = 0;
            //Opening a Connection
            SqlConnection.Open();
            using (SqlConnection)
            {

                //Begin the SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //Alter a table 
                    sqlCommand.CommandText = "Alter table Contact_Person add Date_Value Date";
                    result = sqlCommand.ExecuteNonQuery();
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the last point
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return result;
        }
        /// <summary>
        /// Set the value for date field column in Contact_Person
        /// </summary>
        /// <returns></returns>
        public int UpdateStartDateValueBasedOnContctId()
        {
            int count = 0;
            //Opening a  Transcation
            SqlConnection.Open();
            using (SqlConnection)
            {
                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                   //Set the value of date field based on contact id > 2 
                    sqlCommand.CommandText = "Update Contact_Person Set Date_Value='2019-03-23' where ContactID>2";
                    sqlCommand.ExecuteNonQuery();
                    count++;
                    //Set the value of date field based on contact id <=2
                    sqlCommand.CommandText = "Update Contact_Person Set Date_Value='2021-01-05' where ContactID<=2";
                 
                    sqlCommand.ExecuteNonQuery();
                    count++;
                    sqlTransaction.Commit();
                    Console.WriteLine("Updated Sucessfully!");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Updated!");
                }
            }
            SqlConnection.Close();
            return count;
        }
        /// <summary>
        /// Retrieve the details based on date range
        /// </summary>
        /// <returns></returns>
        public int RetrieveDataBasedOnDateRange()
        {
            //Count the Update details and store it 
            int count = 0;
            //Opening a sql connection
            SqlConnection.Open();
            using (SqlConnection)
            {

                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    sqlCommand.CommandText = @"select FirstName,LastName,Address,City,StateName,ZipCode,PhoneNum,EmailId,Date_Value from Contact_Person
                    where Date_Value between ('2021-01-01') and GetDate()";
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t{5}\t{6}\t{7}", sqlDataReader[0], sqlDataReader[1], sqlDataReader[2], sqlDataReader[3], sqlDataReader[4],sqlDataReader[5],sqlDataReader[6],sqlDataReader[7]);
                            count ++;
                        }
                    }
                    sqlDataReader.Close();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback to the point before exception
                    sqlTransaction.Rollback();
                    Console.WriteLine("Not Retrieve Data Successfully");
                }
            }
            SqlConnection.Close();
            return count;
        }
        /// <summary>
        /// Insert into tables using transcation
        /// </summary>
        /// <returns></returns>
        public int InsertIntoTablesUsingTranscation()
        {
            //Setting the flag to zero
            int flag = 0;
            SqlConnection.Open();
            using (SqlConnection)
            {
                //Begin SQL transaction
                SqlTransaction sqlTransaction = SqlConnection.BeginTransaction();
                SqlCommand sqlCommand = SqlConnection.CreateCommand();
                sqlCommand.Transaction = sqlTransaction;
                try
                {
                    //----------this two tables have relationship if record is inserted in one table,the another table also have relationship to insert--------------------
                    //Insert data into Table(Contact_Person)
                    sqlCommand.CommandText = "Insert into Contact_Person values(1,'Praveen','Kumar','Ranganathan Street','Hyderabad','Telangana',600115,9874587411,'praveen12@yahoo.com','2020-01-09')";
                    sqlCommand.ExecuteNonQuery();
                    //Insert (Relation_Type) Table
                    sqlCommand.CommandText = "Insert into Relation_Type values(1,5)";
                    //Execute the particular query
                    sqlCommand.ExecuteNonQuery();
                    //Commit the transaction if non-conflict occurs
                    sqlTransaction.Commit();
              
                    Console.WriteLine("Inserted Successfully!");
                    //Setting the flag to 1--->Inserted Data  successfully
                  flag = 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Rollback if any error occurs--->Transcation should not be done(Comes back to previous state)
                    sqlTransaction.Rollback();
                   flag = 0;
                }
            }
            SqlConnection.Close();
            return flag;
        }

        public void AddMultipleDataToList()
        {
            try
            {
                string query = @"select AddressBookName,FirstName,LastName,Address,City,StateName,ZipCode,PhoneNum,EmailId,ContactTypeName from AddressBook
     Full JOIN Contact_Person on AddressBook.AddressBookID = Contact_Person.AddressBook_ID
   Full JOIN Relation_Type on Relation_Type.Contact_ID = Contact_Person.ContactID
    Full JOIN Contact_Type on Relation_Type.ContactType_ID = Contact_Type.ContactTypeID";
                SqlCommand sqlCommand = new SqlCommand(query, this.SqlConnection);
                SqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        DisplayEmployeeDetails(sqlDataReader); 
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();      
        }
        public bool ImplementingUsingThread()
        {
            try { 
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            AddMultipleDataToList();
            stopWatch.Stop();
            Console.WriteLine("Duration with Thread excecution : {0} ", stopWatch.ElapsedMilliseconds);
            int elapsedTime = Convert.ToInt32(stopWatch.ElapsedMilliseconds);
            if (elapsedTime != 0)
            {
                return true;
            }
        }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return false;
        }
        public void DisplayEmployeeDetails(SqlDataReader sqlDataReader)
        {
            AddrBookModel addressBook = new AddrBookModel();
            addressBook.firstName = Convert.ToString(sqlDataReader["FirstName"]);
            addressBook.lastName = Convert.ToString(sqlDataReader["LastName"]);
            addressBook.address = Convert.ToString(sqlDataReader["Address"]);
            addressBook.city = Convert.ToString(sqlDataReader["City"]);
            addressBook.stateName = Convert.ToString(sqlDataReader["StateName"]);
            addressBook.zipCode = Convert.ToString(sqlDataReader["ZipCode"]);
            addressBook.phoneNum = Convert.ToInt64(sqlDataReader["PhoneNum"]);
            addressBook.emailId = Convert.ToString(sqlDataReader["EmailId"]);
            addressBook.addrBookName = Convert.ToString(sqlDataReader["AddressBookName"]);
            addressBook.contactTypeName = Convert.ToString(sqlDataReader["ContactTypeName"]);
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6} {7}\t {8}\t {9}\t", addressBook.firstName, addressBook.lastName, addressBook.address, addressBook.city, addressBook.stateName, addressBook.zipCode, addressBook.phoneNum,addressBook.emailId,addressBook.addrBookName,addressBook.contactTypeName);
                addrList.Add(addressBook);
            });
            thread.Start();
        }
    }
}
