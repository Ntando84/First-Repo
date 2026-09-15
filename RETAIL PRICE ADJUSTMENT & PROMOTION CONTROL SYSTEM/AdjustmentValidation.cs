using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public class AdjustmentValidation
    {
        public bool ValidateAdjustment(
            decimal percentage,
            string adjustmentType,
            string reason,
            string approvedBy,
            out string errorMessage)
        {
            // Step 1 : percentage must be greater than zero
            if (percentage <= 0)
            {
                errorMessage = "AdjustmentPercentage must be greater than zero.";
                return false;
            }

            // Step 2 : percentage does not exceed 50%, ceiling set by business.
            if (percentage > 50)
            {
                errorMessage = "AdjustmentPercentage may not exceed 50%.";
                return false;
            }

            // Step 3 : AdjustmentType must exactly either be MARKUP or MARKDOWN.
            // ToUpper() makes the comparison case-sensitive
            if (adjustmentType?.ToUpper() != "MARKUP" &&
                adjustmentType?.ToUpper() != "MARKDOWN")
            {
                errorMessage = "AdjustmentType must either be MARKUP or MARKDOWN.";
                return false;
            }

            // Step 4 : Reason and ApprovedBy cannot be null.
            // IsNullOrWhiteSpace catches both an empty string AND a string made up of
            // only spaces, which IsNullOrEmpty alone would miss.
            if (string.IsNullOrWhiteSpace(reason))
            {
                errorMessage = "Reason is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(approvedBy))
            {
                errorMessage = "ApprovedBy is required.";
                return false;
            }

            // If we reach this line, that means every check has passed.
            errorMessage = null;
            return true;
        }
    }
}
