using System.Configuration;
using System.Data.SqlClient;

namespace Lab07_ADONET_Connection
{
    public static class DbConnectionFactory
    {
        public static SqlConnection Create()
        {
            // Lấy chuỗi kết nối từ file App.config
            string connStr = ConfigurationManager.ConnectionStrings["QLSinhVienDb"].ConnectionString;
            return new SqlConnection(connStr);
        }
    }
}