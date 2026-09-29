using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ContactsDateAccessLayer.clsCountriesData;

namespace ContactsDateAccessLayer
{
    public class clsCountriesData
    {
        public struct stCountryInfo
        {
           public  int ID;
           public string Name;
        }

        public static bool GetCountryByID(int ID , ref stCountryInfo Country)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Select * from Countries
                               Where CountryID =  @CountryID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try

            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    Country.ID = ID;
                    Country.Name = (string)reader["CountryName"];
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception  ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetCountryByName(string CountryName, ref stCountryInfo Country)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"Select * from Countries 
                                Where CountryName = @CountryName";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Country.ID = (int)reader["CountryID"];
                    Country.Name = CountryName;
                }
                else
                {
                    isFound = false;
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
            return isFound;
        }
        public static bool isExistCountryByID(int ID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"Select Found =1 from Countries 
                                Where CountryID = @CountryID";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool testIsCountryExistByName(string CountryName)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = @"Select Found =1 from Countries 
                                Where CountryName = @CountryName";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Message: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static int AddNewCountry(stCountryInfo countryInfo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"INSERT INTO Countries (CountryName)
                             VALUES (@CountryName)
                             select SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName" , countryInfo.Name);

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int insertedID))
                    return insertedID;
                else
                    return -1;

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
            
        }
        public static bool UpdateCountry(stCountryInfo countryInfo)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Update Countries 
                               set CountryName = @Name
                               Where CountryID = @CountryID ";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@Name", countryInfo.Name);
            command.Parameters.AddWithValue("@CountryID", countryInfo.ID);

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
        public static bool DeleteCountry(int CountryID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);
            string Query = @"Delete Countries 
                               Where CountryID = @CountryID ";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

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

        public static DataTable ListCountries()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString);

            string Query = "Select * from Countries";
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
    }
}
