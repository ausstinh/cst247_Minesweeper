using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cst247_Minesweeper.Models.data
{
    public class AccountDataService : DataServiceInterface<UserModel>
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool Create(UserModel newUser)
        {
            bool success = false;

            string queryString = "INSERT INTO dbo.users (FIRSTNAME, LASTNAME, USERNAME, PASSWORD, EMAIL, SEX, AGE, STATE) VALUES (@FIRSTNAME, @LASTNAME, @USERNAME, @PASSWORD, @EMAIL, @SEX, @AGE, @STATE)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 40).Value = newUser.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 40).Value = newUser.LastName;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = newUser.UserName;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = newUser.Password;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 40).Value = newUser.Email;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 10).Value = newUser.Sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.VarChar, 10).Value = newUser.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 10).Value = newUser.State;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.RecordsAffected > 0)
                    {
                        success = true;
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel Read(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> ReadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(UserModel updatedUser)
        {
            throw new NotImplementedException();
        }

        public bool Authenticate(UserModel user)
        {
            bool success = false;

            string queryString = "SELECT * FROM dbo.users WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = user.UserName;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

    }
}