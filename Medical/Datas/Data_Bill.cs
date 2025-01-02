using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical.Mod;
using MySql.Data.MySqlClient;

namespace Medical.Datas
{
    internal class Data_Bill
    {
        private readonly string connectionString;
        public Data_Bill(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public void AddBill(Bill bill)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    string query = @"INSERT INTO bill 
                                     (id,id_patient, date)
                                     VALUES (@id,@patient,@date)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", bill.Id);
                        cmd.Parameters.AddWithValue("@patient", bill.patient.Id);
                        cmd.Parameters.AddWithValue("@date", bill.date);
                        cmd.ExecuteNonQuery();
                    }

                    foreach(Opiration opiration in bill.opirations )
                    {
                         query = @"INSERT INTO bill_opiration 
                                     (id_bill,id_opiration)
                                     VALUES (@bill,@opiration)";
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@bill", bill.Id);
                            cmd.Parameters.AddWithValue("@opiration", opiration.Id);
                            cmd.ExecuteNonQuery();
                        }

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
