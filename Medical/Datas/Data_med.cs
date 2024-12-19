
using Medical.Mod;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Medical.Datas
{
    public class Data_med
    {
        private readonly string connectionString;
        public Data_med(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }
        public List<Medecine> GetAllMedecine()
        {
            List<Medecine> medecine = new List<Medecine>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query_doc = "SELECT * FROM mendicant";


                        MySqlCommand cmd_doc = new MySqlCommand(query_doc, conn);
                        

                        using (MySqlDataReader reader = cmd_doc.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                medecine.Add(new Medecine
                                {
                                    Id_med = reader.GetInt32(0),
                                    Name_med = reader.GetString("med_name"),
                                    Descreption_med = reader.GetString("description"),
                                    dosage_me = reader.GetString("dosage"),
                                   

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

            return medecine;

        }


        public void AddMedecine(int Id_med , string name_med , string desc_med , string dos_med)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO mendicant 
                                     (med_name, description, dosage	)
                                     VALUES (@name, @desc, @dosage )";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name_med);
                        cmd.Parameters.AddWithValue("@desc", desc_med);
                        cmd.Parameters.AddWithValue("@dosage", dos_med);
                        

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


        public void DeleteMedecint(int medecintId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM mendicant WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", medecintId);
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

        public void UpdateMedicent(int Id_med, string name_med, string desc_med, string dos_med)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE mendicant 
                             SET med_name = @name, description = @desc, dosage = @dos 
                                  
                                 
                             WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Id_med);
                        cmd.Parameters.AddWithValue("@name", name_med);
                        cmd.Parameters.AddWithValue("@desc", desc_med);
                        cmd.Parameters.AddWithValue("@dos", dos_med);
                        

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
