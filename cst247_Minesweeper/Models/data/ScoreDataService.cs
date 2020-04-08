using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cst247_Minesweeper.Models.data
{
    public class ScoreDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool Create(ScoreModel score)
        {
            bool success = false;

            string queryString = "INSERT INTO dbo.scores (SCORE, DIFFICULTY, USERID) VALUES (@SCORE, @DIFFICULTY, @USERID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@SCORE", System.Data.SqlDbType.VarChar, 40).Value = score.Score;
                command.Parameters.Add("@DIFFICULTY", System.Data.SqlDbType.VarChar, 40).Value = score.Difficulty;
                command.Parameters.Add("@USERID", System.Data.SqlDbType.VarChar, 40).Value = score.UserId;

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

        public ScoreModel Read(int id)
        {
            ScoreModel score = null;

            string queryString = "SELECT * FROM dbo.scores WHERE ID = @ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        score = new ScoreModel(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return score;
        }

        public List<ScoreModel> ReadAll()
        {
            List<ScoreModel> scores = new List<ScoreModel>();

            string queryString = "SELECT * FROM dbo.scores";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ScoreModel score = new ScoreModel(reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                        scores.Add(score);
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return scores;
        }

        public bool Update(ScoreModel score)
        {
            throw new NotImplementedException();
        }

    }
}