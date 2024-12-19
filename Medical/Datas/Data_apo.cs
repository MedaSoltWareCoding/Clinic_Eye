﻿//using Medical.Mod;
using Medical.Mod;

using MySql.Data.MySqlClient;

using System.Data;
using System.Drawing;
using System.Windows;

namespace Medical.Datas
{
    public class Data_apo
    {
        private readonly string connectionString;



        public Data_apo(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public List<Appointment> GetAllAppointments()

        {

            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM appointment a , patient p where a.id_patient = p.id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                appointments.Add(new Appointment
                                {
                                    Id = reader.GetInt32(0),
                                    patient = new Patient
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
                                    },
                                    date = reader.GetDateTime("date"),
                                    state = reader.GetInt32("state"),
                          
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

            return appointments;
        }



        public void AddAppointment(int Id, Patient patient, DateTime date, int state)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO appointment 
                                     (id_patient, date, state)
                                     VALUES (@patient, @date, @state)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@patient", patient.Id);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@state", state);
    
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

        public void DeleteAppointment(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM appointment WHERE id = @id";
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
        public void UpdateAppointment(int id, Patient patient, DateTime date, int state)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE appointment 
                             SET id_patient = @id_patient, date = @date, state = @state, 
                             WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@id_patient", patient.Id);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@state", state);
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