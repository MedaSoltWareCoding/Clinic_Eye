//using Medical.Mod;
using Medical.Mod;
 
using MySql.Data.MySqlClient;

using System.Data;
using System.Drawing;
using System.Windows;

namespace Medical.Datas
{
    public class DatabaseHelper
    {
        private readonly string connectionString;
        


        public DatabaseHelper(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public List<Patient> GetAllPatients()

        {
            
            List<Patient> patients = new List<Patient>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM patient";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                

                                patients.Add(new Patient
                                {

                                    Name = reader.GetString("pat_name"),
                                    FamilyName = reader.GetString("pat_fname"),
                                    Age = reader.GetInt32("pat_age"),
                                    Gender = reader.GetString("pat_gander"),
                                    Birthday = reader.GetDateTime("pat_birthday"),
                                    City = reader.GetString("pat_city"),
                                    Address = reader.GetString("pat_adress"),
                                    Phone = reader.GetString("pat_phone")
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

            return patients;
        }



        public void AddPatient( string name, string fname, int age, string gender, DateTime? birthday, string city, string address, string phone)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO patient 
                                     (pat_name, pat_fname, pat_age, pat_gander, pat_birthday, pat_city, pat_adress, pat_phone)
                                     VALUES (@name, @fname, @age, @gender, @birthday, @city, @address, @phone)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@fname", fname);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@birthday", birthday.HasValue ? birthday.Value.ToString("yyyy-MM-dd") : DBNull.Value);
                        cmd.Parameters.AddWithValue("@city", city);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@phone", phone);

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

        //private void LoadPatients()
        //{
        //    using (MySqlConnection conn = new MySqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string query = "SELECT * FROM patient";
        //            MySqlCommand cmd = new MySqlCommand(query, conn);
        //            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);
                    
        //            PatientsDataGrid.ItemsSource = dt.DefaultView;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: " + ex.Message);
        //        }
        //    }
        //}

    }
}
