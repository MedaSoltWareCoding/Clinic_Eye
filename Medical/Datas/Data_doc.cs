
using Medical.Mod;
using MySql.Data.MySqlClient;
using System.Windows;


namespace Medical.Datas
{
    public class Data_doc
    {
        private readonly string connectionString;
        public Data_doc(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public List<Doctors> GetAllDoctors()
        {
            List<Doctors> doctor = new List<Doctors>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query_doc = "SELECT * FROM doctors";


                        MySqlCommand cmd_doc = new MySqlCommand(query_doc, conn);


                        using (MySqlDataReader reader = cmd_doc.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                doctor.Add(new Doctors
                                {
                                    Id_doc = reader.GetInt32(0),
                                    Name_doc = reader.GetString("name"),
                                    Familyname_doc = reader.GetString("fname"),
                                    Age_doc = reader.GetInt32("age"),
                                    Phone_doc = reader.GetString("phone"),
                                    Adress_doc = reader.GetString("adress"),
                                    Branch_doc = reader.GetString("branch"),
                                    
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

            return doctor;

        }


        public void AddDoctor(int Id_doc, string name_dco, string fname_doc, int age_doc, string address_doc, string phone_doc,  string branch_doc)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO doctors 
                                     (name, fname, age, adress,phone,branch)
                                     VALUES (@name, @fname, @age ,@address, @phone ,@branch)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", name_dco);
                        cmd.Parameters.AddWithValue("@fname", fname_doc);
                        cmd.Parameters.AddWithValue("@age", age_doc);
                        cmd.Parameters.AddWithValue("@address", address_doc);
                        cmd.Parameters.AddWithValue("@phone", phone_doc);
                        cmd.Parameters.AddWithValue("@branch", branch_doc);

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


        public void DeleteDoctor(int DoctortId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM doctors WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", DoctortId);
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

        public void UpdateDoctor(int Id_doc, string name_dco, string fname_doc, int age_doc, string phone_doc, string address_doc, string branch_doc)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"UPDATE doctors 
                             SET name = @name, fname = @fname, age = @age, 
                                  
                                 adress = @address, phone = @phone , branch = @branch
                             WHERE id = @id";
                    
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Id_doc);
                        cmd.Parameters.AddWithValue("@name", name_dco);
                        cmd.Parameters.AddWithValue("@fname", fname_doc);
                        cmd.Parameters.AddWithValue("@age", age_doc);
                        cmd.Parameters.AddWithValue("@address", address_doc);
                        cmd.Parameters.AddWithValue("@phone", phone_doc);
                        cmd.Parameters.AddWithValue("@branch", branch_doc);

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
     
    
    
