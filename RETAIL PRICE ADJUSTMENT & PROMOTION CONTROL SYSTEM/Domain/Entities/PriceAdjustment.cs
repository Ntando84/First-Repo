using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities
{
    public class PriceAdjustment
    {
        /*Represents one row of price-change history.
         * Mirrors the PriceAdjustment table 
         */
        public int AdjustmentId { get; set; }
        public int ProductId { get; set; }
        public string AdjustmentType { get; set; } // "MARKUP" or "MARKDOWN"
        public decimal AdjustmentPercentage { get; set; } 
        public decimal PreviousPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; }
        public string ApprovedBy { get; set; }

        // Optional navigation property to Product.
        // Populated when a query joins to Product.
        public Product Product { get; set; }

        public PriceAdjustment() { }

        public PriceAdjustment(int adjustmentId, int productId, string adjustmentType,
            decimal adjustmentPercentage, decimal previousPrice,
            decimal newPrice, DateTime adjustmentdate,
            string reason, string approvedBy)
        {
            ProductId = productId;
            AdjustmentId = adjustmentId;
            AdjustmentType = adjustmentType;
            AdjustmentPercentage = adjustmentPercentage;
            PreviousPrice = previousPrice;
            NewPrice = newPrice;
            AdjustmentDate = adjustmentdate;
            Reason = reason;
            ApprovedBy = approvedBy;
        }

        // Calculated purely from stored data - handy for reporting/console output
        // without repeating the subtraction logic elsewhere.
        public decimal PriceChangeAmount => NewPrice - PreviousPrice;

        public override string ToString() =>
            $"[{AdjustmentDate:g}] {AdjustmentType} {AdjustmentPercentage}%: " +
            $"R{PreviousPrice:F2} -> R{NewPrice:F2} ({Reason}, approved by {ApprovedBy})";
    }
}
