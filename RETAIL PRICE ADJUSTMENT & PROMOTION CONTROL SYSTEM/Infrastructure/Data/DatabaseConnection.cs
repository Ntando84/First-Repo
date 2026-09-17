using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Data
{
    public static class DatabaseConnection
    {
        // Update this to match your SQL Server instance.
        private const string ConnectionString =
            "Server=localhost;Database=RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM;" +
            "Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection() => 
            new SqlConnection(ConnectionString);
    }
}
