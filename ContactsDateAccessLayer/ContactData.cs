using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactsDateAccessLayer
{
    public class clsContactDataAccess
    {
        public struct stInfo
        {
            public int ID;
            public string FirstName;
            public string LastName;
            public string Email;
            public string Phone;
            public string Address;
            public string ImagePath;
            public int CountryID;
            public DateTime DateOfBirth;

        }

        public static bool GetContactInfoByID(int ID , ref stInfo Info)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Select * from Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query , connection);
            command.Parameters.AddWithValue("@ContactID", ID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    Info.ID = ID;
                    Info.FirstName = (string)reader["FirstName"];
                    Info.LastName = (string)reader["LastName"];
                    Info.Email = (string)reader["Email"];
                    Info.Phone = (string)reader["Phone"];
                    Info.Address = (string)reader["Address"];
                    Info.DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Info.CountryID = (int)reader["CountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                    {
                      Info.ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        Info.ImagePath = "";
                    }

                }
                else
                {
                    isFound = false;
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewContact(stInfo DALInfo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address,DateOfBirth, CountryID,ImagePath) VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@DateOfBirth, @CountryID,@ImagePath)SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@FirstName" , DALInfo.FirstName);
            command.Parameters.AddWithValue("@LastName", DALInfo.LastName);
            command.Parameters.AddWithValue("@Email", DALInfo.Email);
            command.Parameters.AddWithValue("@Phone", DALInfo.Phone);
            command.Parameters.AddWithValue("@Address", DALInfo.Address);
            command.Parameters.AddWithValue("@DateOfBirth", DALInfo.DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", DALInfo.CountryID);

            if (DALInfo.ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", DALInfo.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath",System.DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString() , out int insertedID))
                {
                    return insertedID;
                }
                else
                {
                    return -1;
                }

                    
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                connection.Close();
            }
            return -1;
        }
        public static bool UpdateContact(stInfo DALInfo) {

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Update Contacts  
                            set FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone, 
                                Address = @Address, 
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath =@ImagePath
                                where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ContactID", DALInfo.ID);
            command.Parameters.AddWithValue("@FirstName" , DALInfo.FirstName);
            command.Parameters.AddWithValue("@LastName", DALInfo.LastName);
            command.Parameters.AddWithValue("@Email", DALInfo.Email);
            command.Parameters.AddWithValue("@Phone", DALInfo.Phone);
            command.Parameters.AddWithValue("@Address", DALInfo.Address);
            command.Parameters.AddWithValue("@DateOfBirth", DALInfo.DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", DALInfo.CountryID);

            if (DALInfo.ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", DALInfo.ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            int rowAffected = 0;
            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error Message: "+ex.Message);
                
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }
        public static bool DeleteContact(int ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Delete Contacts 
                             Where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query , connection);
            command.Parameters.AddWithValue("@ContactID" , ID );

            int rowAffected = 0;
            try
            {
                connection.Open();

                rowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return (rowAffected > 0);
        }
        public static DataTable ListContacts()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Select * From Contacts";

            SqlCommand command = new SqlCommand(Query, connection);

            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static bool IsContactExistByID(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Select Found =1 from Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
