using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Medical.Mod;
using MySql.Data.MySqlClient;

namespace Medical.Datas
{
    internal class Data_Session
    {
        private readonly MySqlConnection connection;
        public Data_Session(MySqlConnection connection)
        {
            this.connection = connection;
           // connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public void AddSession(List<Session> sessions)
        {
            using (connection)
            { 
                foreach (Session session in sessions) 
     
                 {   try
                    {


                        string query = @"INSERT INTO session 
                                         (id_pescription,id_medicant,description)
                                         VALUES (@pescription, @medicant,@description)";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@pescription", session.pescriptionId);
                            cmd.Parameters.AddWithValue("@medicant", session.medecine.Id_med);
                            cmd.Parameters.AddWithValue("@description", session.descrition);
                            cmd.ExecuteNonQuery();

                        }
                    }
                    catch (MySqlException ex)
                    {
                        throw new Exception("Database session error: " + ex.Message, ex);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error: " + ex.Message, ex);
                    }
                }
            }
        }
    }
}
