using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FabrikamBackend.DataAccess
{
    public class DatabaseAccess
    {
        private SqlConnection _connection;
        private readonly string connectionString = "your connection string";

        public bool VerifyLogin(string username, string password)
        {
            if (OpenSqlConnection())
            {
                var query = "SELECT Password FROM dbo.Admin WHERE Username=@username";

                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    try
                    {
                        var result = command.ExecuteScalar().ToString();

                        if (result == password)
                        {
                            CloseSqlConnection();
                            return true;
                        }
                        else
                        {
                            CloseSqlConnection();
                            return false;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        CloseSqlConnection();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public List<(string, string, string)> GetBlogPosts()
        {
            var blogPosts = new List<(string, string, string)>();

            if (OpenSqlConnection())
            {
                var query = "SELECT Title, Image, Body FROM dbo.Blog";

                using (var command = new SqlCommand(query, _connection))
                {
                    try
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            blogPosts.Add((reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                        }
                        CloseSqlConnection();

                        return blogPosts;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        CloseSqlConnection();
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public bool AddBlogPost(string title, string image, string body)
        {
            if (OpenSqlConnection())
            {
                var query = "INSERT INTO dbo.Blog (Title, Image, Body) VALUES (@title, @image, @body)";

                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@image", image);
                    command.Parameters.AddWithValue("@body", body);

                    try
                    {
                        command.ExecuteNonQuery();
                        CloseSqlConnection();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        CloseSqlConnection();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        private bool OpenSqlConnection()
        {
            _connection = new SqlConnection(connectionString);
            try
            {
                _connection.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private bool CloseSqlConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
