using System.Data.SqlClient;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Data;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure.Repositories
{
    public class PriceAdjustmentRepository : IPriceAdjustmentRepository
    {
        public void Insert(PriceAdjustment adjustment)
        {
            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                INSERT INTO PriceAdjustment
                    (ProductId, AdjustmentType, AdjustmentPercentage, PreviousPrice,
                     NewPrice, AdjustmentDate, Reason, ApprovedBy)
                VALUES
                    (@ProductId, @AdjustmentType, @AdjustmentPercentage, @PreviousPrice,
                     @NewPrice, GETDATE(), @Reason, @ApprovedBy);";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductId", adjustment.ProductId);
            cmd.Parameters.AddWithValue("@AdjustmentType", adjustment.AdjustmentType);

            cmd.Parameters.AddWithValue("@AdjustmentPercentage", adjustment.AdjustmentPercentage);
            cmd.Parameters.AddWithValue("@PreviousPrice", adjustment.PreviousPrice);
            cmd.Parameters.AddWithValue("@NewPrice", adjustment.NewPrice);
            cmd.Parameters.AddWithValue("@Reason", adjustment.Reason);
            cmd.Parameters.AddWithValue("@ApprovedBy", adjustment.ApprovedBy);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public List<PriceAdjustment> GetHistoryForProduct(int productId)
        {
            var history = new List<PriceAdjustment>();

            using var conn = DatabaseConnection.GetConnection();
            const string sql = @"
                SELECT AdjustmentId, ProductId, AdjustmentType, AdjustmentPercentage,
                       PreviousPrice, NewPrice, AdjustmentDate, Reason, ApprovedBy
                FROM PriceAdjustment
                WHERE ProductId = @ProductId
                ORDER BY AdjustmentDate DESC;";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                history.Add(new PriceAdjustment
                {
                    AdjustmentId = reader.GetInt32(0),
                    ProductId = reader.GetInt32(1),
                    AdjustmentType = reader.GetString(2),
                    AdjustmentPercentage = reader.GetDecimal(3),
                    PreviousPrice = reader.GetDecimal(4),
                    NewPrice = reader.GetDecimal(5),
                    AdjustmentDate = reader.GetDateTime(6),
                    Reason = reader.GetString(7),
                    ApprovedBy = reader.GetString(8)
                });
            }
            return history;
        }

    }
}
