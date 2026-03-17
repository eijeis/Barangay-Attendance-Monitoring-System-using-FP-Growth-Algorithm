using Microsoft.Data.SqlClient;

namespace BAMS.Modules
{
    public class DatabaseConnection
    {
        private static string connectionString =
            "Server=localhost;Database=BAMS_DB;User Id=eijeizs;Password=aspiringrapper3;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}