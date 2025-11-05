using Microsoft.Data.SqlClient;

namespace MarketHup.Models
{
    public class Connection
    {
        public static SqlConnection ServerConnect
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-J8O87PF\\SQLEXPRESS;Database=Wep_Eticaret_CoreDB;Trusted_Connection=True;TrustServerCertificate=True;");
                return sqlConnection;
            }
        }
    }
}
