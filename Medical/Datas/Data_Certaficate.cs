using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical.Mod;
using MySql.Data.MySqlClient;

namespace Medical.Datas
{
    internal class Data_Certaficate
    {
        private readonly string connectionString;
        public Data_Certaficate(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public void AddCertaficate(Certaficate certaficate)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    string query = @"INSERT INTO certaficate 
                                     (id,id_patient,content, date)
                                     VALUES (@id,@patient, @content,@date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", certaficate.Id);
                        cmd.Parameters.AddWithValue("@patient", certaficate.patient.Id);
                        cmd.Parameters.AddWithValue("@content", certaficate.contant);
                        cmd.Parameters.AddWithValue("@date", certaficate.date);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Database pescription error: " + ex.Message, ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: " + ex.Message, ex);
                }
            }
        }
    }
}
