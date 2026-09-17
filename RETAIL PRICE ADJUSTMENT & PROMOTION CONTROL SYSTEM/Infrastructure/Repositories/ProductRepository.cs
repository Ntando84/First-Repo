using System.Data.SqlClient;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Data;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Repositories
{
    /*Repository/Date Access layer. Every method here uses a parameterised query
     *(@ProductCode, @ProductId, ...) - values are sent to the SQL Server as data,
     *never glued into the command text, which is what closes off SQL injection.
     */
    public class ProductRepository : IProductRepository
    {
        public Product FindByCode(string productCode)
        {
            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                SELECT ProductId, ProductCode, ProductName, CategoryId,
                       UnitCost, CurrentSellingPrice, MinimumSellingPrice
                FROM Product
                WHERE ProductCode = @ProductCode;";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductCode", productCode);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null; // no matching product

            return new Product
            (
                reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5),
                reader.GetDecimal(6)
            );
        }

        public Product FindById(int productId)
        {
            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                SELECT ProductId, ProductCode, ProductName, CategoryId,
                       UnitCost, CurrentSellingPrice, MinimumSellingPrice
                FROM Product
                WHERE ProductId = @ProductId;";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;
            return new Product
            (
                reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                reader.GetInt32(3), reader.GetDecimal(4), reader.GetDecimal(5),
                reader.GetDecimal(6)
            );
        }

        // Always filter by ProductId (unique, numeric) - never by name.
        public void UpdateSellingPrice(int productId, decimal newPrice)
        {
            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                UPDATE Product
                SET CurrentSellingPrice = @NewPrice
                WHERE ProductId = @ProductId;";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NewPrice", newPrice);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
