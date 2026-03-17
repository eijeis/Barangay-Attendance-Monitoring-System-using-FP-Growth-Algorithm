//using System;
//using System.Windows.Forms;
//using Microsoft.Data.SqlClient;

//namespace BAMS.Repositories
//{
//    internal class AdminRepository
//    {
//        private readonly string connectionString =
//            "Server=localhost;Database=BAMS_DB;User Id=eijeizs;Password=aspiringrapper3;TrustServerCertificate=True;";

//        public bool Login(string username, string password)
//        {
//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {

//                conn.Open();

//                string query = @"SELECT COUNT(*)
//                                FROM Admin 
//                                WHERE Username = @username 
//                                AND Password = @password";

//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@username", username.Trim());
//                    cmd.Parameters.AddWithValue("@password", password.Trim());

//                    int result = (int)cmd.ExecuteScalar();

//                    return result > 0;
//                }
//            }
//        }
//    }
//}

using BAMS.Modules;
using Microsoft.Data.SqlClient;

namespace BAMS.Repositories
{
    internal class AdminRepository
    {
        public bool Login(string username, string password)
        {
            using SqlConnection conn = DatabaseConnection.GetConnection();

            string query = @"SELECT COUNT(*)
                            FROM Admin
                            WHERE Username=@username
                            AND Password=@password";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@username", username.Trim());
            cmd.Parameters.AddWithValue("@password", password.Trim());

            conn.Open();

            int result = (int)cmd.ExecuteScalar();

            return result > 0;
        }
    }
}