
using Medical.Mod;
using Medical.View;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Medical.Datas
{
    public class Data_Opiration
    {   
        private readonly string connectionString;
        public Data_Opiration(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }
        public List<Opiration> GetAllOpirations()
        {
            List<Opiration> opirations = new List<Opiration>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query_doc = "SELECT * FROM Opiration";


                        MySqlCommand cmd_doc = new MySqlCommand(query_doc, conn);


                        using (MySqlDataReader reader = cmd_doc.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                               
                                opirations.Add(new Opiration
                                {
                                    Id = reader.GetInt32("id"),
                                    title = reader.GetString("title"),
                                    description = reader.GetString("description"),
                                    price = reader.GetDouble("price"),
                                });
                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            return opirations;

        }


        public void AddOpiration(Opiration opiration)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO opiration 
                                     (title,description, price)
                                     VALUES (@title, @desc, @price )";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", opiration.title);
                        cmd.Parameters.AddWithValue("@desc", opiration.description);
                        cmd.Parameters.AddWithValue("@price", opiration.price);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Database error: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message, ex);
                }
            }
        }


        public void DeleteOpiration(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM opiration WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Database error: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message, ex);
                }
            }
        }

        public void UpdateOpiration(Opiration opiration)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE opiration 
                             SET title = @title, description = @desc, price = @price 
                             WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", opiration.Id);
                        cmd.Parameters.AddWithValue("@title", opiration.title);
                        cmd.Parameters.AddWithValue("@desc", opiration.description);
                        cmd.Parameters.AddWithValue("@price", opiration.price);


                        cmd.ExecuteNonQuery();
                    }
                }

                catch (MySqlException ex)
                {
                    throw new Exception("Database error: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
