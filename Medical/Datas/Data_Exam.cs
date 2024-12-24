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
    public  class Data_Exam
    {
        private readonly string connectionString;
        public Data_Exam(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
        }

        public List<Exams> GetAllExams()
        {
            List<Exams> exams = new List<Exams>();


            using (MySqlConnection connection = new MySqlConnection(connectionString))

            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query_doc = "SELECT * FROM exams";


                        MySqlCommand cmd_doc = new MySqlCommand(query_doc, conn);


                        using (MySqlDataReader reader = cmd_doc.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                exams.Add(new Exams
                                {
                                    Id = reader.GetInt32("id"),
                                    Cylinidrical_OD = reader.GetString("Cylinidrical_OD"),
                                    Spherical_OD = reader.GetString("Spherical_OD"),
                                    Add_power_OD = reader.GetString("Add_power_OD"),
                                    Axis_OD = reader.GetString("Axis_OD"),
                                    Cylinidrical_OS = reader.GetString("Cylinidrical_OS"),
                                    Spherical_OS = reader.GetString("Spherical_OS"),
                                    Add_power_OS = reader.GetString("Add_power_OS"),
                                    Axis_OS = reader.GetString("Axis_OS"),
                                    Base_cruve_OD = reader.GetString("Base_cruve_OD"),
                                    Diameterer_OD = reader.GetString("Diameterer_OD"),
                                    Power_OD = reader.GetString("Power_OD"),
                                    Brand_type_OD = reader.GetString("Brand_type_OD"),
                                    Base_cruve_OS = reader.GetString("Base_cruve_OS"),
                                    Diameterer_OS = reader.GetString("Diameterer_OS"),
                                    Power_OS = reader.GetString("Power_OS"),
                                    Brand_type_OS = reader.GetString("Brand_type_OS"),
                                    DV = reader.GetString("DV"),
                                    NV = reader.GetString("NV"),
                                    CV = reader.GetString("CV"),
                                    SPH = reader.GetString("SPH"),
                                    CYL = reader.GetString("CYL"),
                                    AXIS = reader.GetString("AXIS"),
                                    PDP = reader.GetString("PDP"),
                                    NDP = reader.GetString("NDP"),
                                    CTR = reader.GetString("CTR"),
                                    Phorias = reader.GetString("Phorias"),
                                    Steropsis = reader.GetString("Steropsis")


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

            return exams;
        }

        public void AddExams(int Id, string Cy_OD, string Sph_OD, string Addp_OD,string Ax_OD , string Cy_OS, string Sph_OS, string Addp_OS , string Ax_OS , string Bas_OD ,string Di_OD , string Po_OD , string Br_OD, string Bas_OS, string Di_OS, string Po_OS, string Br_OS,
            string DV , string NV , string CV , string SPH , string CYL , string Axis , string PDP , string NDP , string CTR , string Phorias , string Steropsis)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"INSERT INTO exams 
                                   (id ,Cylinidrical_OD, Spherical_OD, Add_power_OD, 
                                    Axis_OD,Cylinidrical_OS,Spherical_OS ,Add_power_OS,
                                    Axis_OS,Base_cruve_OD,Diameterer_OD,Power_OD,Brand_type_OD,
                                    Base_cruve_OS,Diameterer_OS,
                                    Power_OS,Brand_type_OS,DV,NV,CV,SPH,CYL,AXIS,PDP,NDP,CTR ,Phorias ,Steropsis)
                                    VALUES (@id, @Cylinidrical_OD, @Spherical_OD ,@Add_power_OD, @Axis_OD ,@Cylinidrical_OS,@Spherical_OS,
                                    @Add_power_OS,@Axis_OS,@Base_cruve_OD,@Diameterer_OD,@Power_OD,@Brand_type_OD,@Base_cruve_OS,@Diameterer_OS,
                                    @Power_OS,@Brand_type_OS,@DV,@NV,@CV,@SPH,@CYL,@AXIS,@PDP,@NDP,@CTR ,@Phorias ,@Steropsis )";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Id);
                        cmd.Parameters.AddWithValue("@Cylinidrical_OD", Cy_OD);
                        cmd.Parameters.AddWithValue("@Spherical_OD", Sph_OD);
                        cmd.Parameters.AddWithValue("@Add_power_OD", Addp_OD);
                        cmd.Parameters.AddWithValue("@Axis_OD", Ax_OD);
                        cmd.Parameters.AddWithValue("@Cylinidrical_OS", Cy_OS);
                        cmd.Parameters.AddWithValue("@Spherical_OS", Sph_OS);
                        cmd.Parameters.AddWithValue("@Add_power_OS", Addp_OS);
                        cmd.Parameters.AddWithValue("@Axis_OS", Ax_OS);
                        cmd.Parameters.AddWithValue("@Base_cruve_OD", Bas_OD);
                        cmd.Parameters.AddWithValue("@Diameterer_OD", Di_OD);
                        cmd.Parameters.AddWithValue("@Power_OD", Po_OD);
                        cmd.Parameters.AddWithValue("@Brand_type_OD", Br_OD);
                        cmd.Parameters.AddWithValue("@Base_cruve_OS", Bas_OS);
                        cmd.Parameters.AddWithValue("@Diameterer_OS", Di_OS);
                        cmd.Parameters.AddWithValue("@Power_OS", Po_OS);
                        cmd.Parameters.AddWithValue("@Brand_type_OS", Br_OS);
                        cmd.Parameters.AddWithValue("@DV", DV);
                        cmd.Parameters.AddWithValue("@NV", NV);
                        cmd.Parameters.AddWithValue("@CV", CV);
                        cmd.Parameters.AddWithValue("@SPH", SPH);
                        cmd.Parameters.AddWithValue("@CYL", CYL);
                        cmd.Parameters.AddWithValue("@AXIS", Axis);
                        cmd.Parameters.AddWithValue("@PDP", PDP);
                        cmd.Parameters.AddWithValue("@NDP", NDP);
                        cmd.Parameters.AddWithValue("@CTR", CTR);
                        cmd.Parameters.AddWithValue("@Phorias", Phorias);
                        cmd.Parameters.AddWithValue("@Steropsis", Steropsis);




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





        public void DeleteExams(int ExamsId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM exams WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", ExamsId);
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
