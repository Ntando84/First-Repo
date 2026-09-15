using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public class PricingAdjustment
    {
        //
        public decimal CalculateNewPrice(
            decimal currentPrice,
            string adjustmentType,
            decimal percentage)
        {
            decimal newPrice = 0;
            // Adjustment amount is calculated the same for a MARKUP or MARKDOWN
            decimal adjustmentAmount = currentPrice * percentage / 100;

            if (adjustmentType.ToUpper() == "MARKUP")
                newPrice = currentPrice + adjustmentAmount;

            if (adjustmentType.ToUpper() == "MARKDOWN")
                newPrice = currentPrice - adjustmentAmount;

            return newPrice;
        }


        /*
         * Worked example, step by step
         * Current Selling Price = R1000
         * AdjustmentType        = MARKDOWN
         * AdjustmentPercentage  = 10%
         * 
         * Step 1 - AdjustmentAmount = CurrentSellingPrice * Percentage / 100
         *                           = 1000 * 10 /100
         *                           = R100
         * Step 2 - MARKDOWN means SUBTRACT the amount:
         *          NewPrice = CurrentSellingPrice - AdjustmentAmount
         *                   = 1000 - 100
         *                   = R900
         */

        public void ProcessPriceAdjustment(
            string productCode,
            string adjustmentType,
            decimal percentage,
            string reason,
            string approvedBy)
        {
            // 1. Confirm that the product is real/exists?
            if (!ValidateProductExists(productCode, 
                out var product,
                out var errorMessage))
            {
                Console.WriteLine(errorMessage);
                return; // stop immediately - nothing below this line runs
            }

            // 2. Confirm the requested adjustment itself is valid?
            if (!ValidateAdjustment(percentage,
                adjustmentType,
                reason,
                approvedBy,
                out errorMessage))
            {
                Console.WriteLine(errorMessage);
                return; // stop immediately - nothing below this line runs
            }

            // 3. Only now can we calculate a new price.
            decimal newPrice = CalculateNewPrice(product.CurrentSellingPrice,
                adjustmentType, percentage);

        }
    }
}
