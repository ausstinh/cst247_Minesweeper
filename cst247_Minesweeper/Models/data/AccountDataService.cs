using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cst247_Minesweeper.Models.data
{
    public class AccountDataService
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
                    connection.Close();
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
            UserModel user = null;

            string queryString = "SELECT * FROM dbo.users WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar, 40).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        user = new UserModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetInt32(9));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return user;
        }

        public List<UserModel> ReadAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(UserModel updatedUser)
        {
            bool success = false;

            string queryString = "UPDATE * FROM dbo.users SET FIRSTNAME = @FIRSTNAME, LASTNAME = @LASTNAME, USERNAME = @USERNAME, PASSWORD = @PASSWORD, EMAIL = @EMAIL, SEX = @SEX, AGE = @AGE, STATE = @STATE, ACTIVEGAMEID = @ACTIVEGAMEID WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 40).Value = updatedUser.FirstName;
                command.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 40).Value = updatedUser.LastName;
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 40).Value = updatedUser.UserName;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 40).Value = updatedUser.Password;
                command.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 40).Value = updatedUser.Email;
                command.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 10).Value = updatedUser.Sex;
                command.Parameters.Add("@AGE", System.Data.SqlDbType.VarChar, 10).Value = updatedUser.Age;
                command.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 10).Value = updatedUser.State;
                command.Parameters.Add("@ACTIVEGAMEID", System.Data.SqlDbType.VarChar, 10).Value = updatedUser.ActiveGameId;
                command.Parameters.Add("@ID", System.Data.SqlDbType.VarChar, 10).Value = updatedUser.Id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.RecordsAffected > 0)
                    {
                        success = true;
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return success;
        }

        public int Authenticate(UserModel user)
        {
            int loggedInId = -1;

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
                        reader.Read();
                        loggedInId = reader.GetInt32(0);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return loggedInId;
        }

    }
}