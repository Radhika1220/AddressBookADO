using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookADO
{
    public class Transcation
    {
        //Give path for Database Connection
        public static string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AddressBookSystem;Integrated Security=True;";
        //Represents a connection to Sql Server Database
        SqlConnection SqlConnection = new SqlConnection(connection);
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

    }
}
