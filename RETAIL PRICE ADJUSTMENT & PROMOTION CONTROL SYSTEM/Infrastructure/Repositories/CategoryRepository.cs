using System.Data.SqlClient;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Data;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Category FindById(int categoryId)
        {
            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                SELECT CategoryId, CategoryName, CategoryDescription
                FROM Category
                WHERE CategoryId = @CategoryId;";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;
            return new Category
            {
                CategoryId = reader.GetInt32(0),
                CategoryName = reader.GetString(1),
                CategoryDescription = reader.IsDBNull(2) ? null : reader.GetString(2)
            };
        }
    }
}
