﻿//using Medical.Mod;
using Medical.Mod;

using MySql.Data.MySqlClient;

using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

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
                        string query = "SELECT * , p.id as patid FROM appointment a , patient p where a.id_patient = p.id order by a.date";
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
                                        Id = reader.GetInt32("patid"),
                                        Name = reader.GetString("pat_name"),
                                        FamilyName = reader.GetString("pat_fname"),
                                        Age = reader.GetInt32("pat_age"),
                                        Gender = reader.GetString("pat_gander"),
                                        Birthday = reader.GetDateTime("pat_birthday"),
                                        City = reader.GetString("pat_city"),
                                        Address = reader.GetString("pat_adress"),
                                        Phone = reader.GetString("pat_phone"),


                                    },
                                    date = reader.GetDateTime("date"),
                                    state = reader.GetInt32("state"),
                                    time = reader.GetDateTime("time"),



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



        public void AddAppointment(int Id, Patient patient, DateTime date, int state , DateTime time)
        {
            MessageBox.Show(time + "", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    
                    connection.Open();
                    string query = @"INSERT INTO appointment 
                                     (id_patient, date,time, state)
                                     VALUES (@patient, @date,@time, @state)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@patient", patient.Id);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@state", state);
                        cmd.Parameters.AddWithValue("@time", time);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Database appointment error: " + ex.Message, ex);
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
        public void UpdateAppointment(Appointment app)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE appointment 
                             SET id_patient = @id_patient, date = @date,time = @time, state = @state 
                             WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", app.Id);
                        cmd.Parameters.AddWithValue("@id_patient", app.patient.Id);
                        cmd.Parameters.AddWithValue("@date",app.date);
                        cmd.Parameters.AddWithValue("@time",app.time);
                        cmd.Parameters.AddWithValue("@state",app.state);
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