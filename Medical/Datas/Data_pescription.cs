using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical.Mod;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;

namespace Medical.Datas
{
    internal class Data_pescription
    {
        private readonly string connectionString;
        public Data_pescription(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public List<Pescription> GetAllPescriptions()
        {
            List<Pescription> pescriptions = new List<Pescription>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query_doc = "SELECT * FROM pescription ";


                        MySqlCommand cmd_doc = new MySqlCommand(query_doc, conn);


                        using (MySqlDataReader reader = cmd_doc.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                pescriptions.Add(new Pescription
                                {
                                    Id = reader.GetInt32(0),
                                    patient = new Patient {
                                        Id = reader.GetInt32("idpat"),
                                        Address = reader.GetString("address"),
                                        Name = reader.GetString("patname"),
                                        FamilyName = reader.GetString("patfamily"),
                                        Age = reader.GetInt32("patage"),
                                        Gender = reader.GetString("patgender"),
                                        City = reader.GetString("patcity"),
                                        Phone = reader.GetString("patphone"),
                                        Birthday = reader.GetDateTime("patbirth"),
                                    },
                                    doctor = new Doctors
                                    {
                                        Id_doc = reader.GetInt32(0),
                                        Name_doc = reader.GetString("docname"),
                                        Familyname_doc = reader.GetString("docfname"),
                                        Age_doc = reader.GetInt32("docage"),
                                        Phone_doc = reader.GetString("docphone"),
                                        Adress_doc = reader.GetString("docadress"),
                                        Branch_doc = reader.GetString("docbranch"),

                                    },
                                    date = reader.GetDateTime("date"),
                                    medcines = new List<Session>()

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

            return pescriptions;
        }

        public void AddPescription(Pescription pescription)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    string query = @"INSERT INTO pescription 
                                     (id,id_patient,id_doctor, date)
                                     VALUES (@id,@patient, @doctor,@date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", pescription.Id);
                        cmd.Parameters.AddWithValue("@patient", pescription.patient.Id);
                        cmd.Parameters.AddWithValue("@doctor", pescription.doctor.Id_doc);
                        cmd.Parameters.AddWithValue("@date", pescription.date);
                        cmd.ExecuteNonQuery();
                        Data_Session data_Session = new Data_Session(connection);
                        data_Session.AddSession(pescription.medcines);
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
