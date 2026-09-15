using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public class PriceAdjustment
    {
        public int AdjustmentId { get; set; }
        public int ProductId { get; set; }
        public string? AdjustmentType { get; set; }
        public decimal AdjustmentPercentage { get; set; } // could be int??
        public decimal PreviousPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime? Adjustmentdate { get; set; }
        public string? Reason { get; set; }
        public string? ApprovedBy { get; set; }

        // May include a navigation property to Product
    }
}
