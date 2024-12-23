﻿//using Medical.Mod;
using Medical.Mod;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Medical.Datas
{
    public class Data_pat
    {
        private readonly string connectionString;
        


        public Data_pat(string server, string database, string username, string password)
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
                        string query_pat = "SELECT * FROM patient";
                      

                        MySqlCommand cmd_pat = new MySqlCommand(query_pat, conn);
                        

                        using (MySqlDataReader reader = cmd_pat.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                

                                patients.Add(new Patient
                                {
                                    Id = reader.GetInt32(0),
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













        public void AddPatient(int Id, string name, string fname, int age, string gender, DateTime? birthday, string city, string address, string phone)
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

        public void DeletePatient(int patientId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM patient WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", patientId);
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
        public void UpdatePatient(int patientId, string name, string fname, int age, string gender, DateTime? birthday, string city, string address, string phone)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE patient 
                             SET pat_name = @name, pat_fname = @fname, pat_age = @age, 
                                 pat_gander = @gender, pat_birthday = @birthday, 
                                 pat_city = @city, pat_adress = @address, pat_phone = @phone 
                             WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", patientId);
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








    }






}
