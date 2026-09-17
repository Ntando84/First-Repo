using System;
using System.Collections.Generic;
using System.Text;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Models;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Services
{
    public class PriceAdjustmentService : IPriceAdjustmentService, IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IPriceAdjustmentRepository _adjustmentRepository;


        public PriceAdjustmentService(IProductRepository productRepository,
            IPriceAdjustmentRepository adjustmentRepository)
        {
            _productRepository = productRepository;
            _adjustmentRepository = adjustmentRepository;
        }


        // Step 1 : Search the repository for the product
        // Ask the repository(data access layer) to look the product up
        public Product GetProductByCode(string productCode)
        {
            return _productRepository.FindByCode(productCode);
        }



        // -------------- CHECK 1 : Does the product exist? ------------
        // Step 2 : Decide whether processing can continue
        // 'out' parameters allow this one method to hand back BOTH
        // the product (if found) AND an error message (if not found)
        public bool ValidateProductExists(
            string productCode,
            out Product product,
            out string errorMessage)
        {
            product = _productRepository.FindByCode(productCode);

            if (product == null)
            {
                // No product was found for this code -> stop here.
                // Nothing below this point should ever run for an invalid code.
                errorMessage = $"Product with code '{productCode}' does not exist.";
                return false;
            }

            errorMessage = null;
            return true;
        }


        // --------------- CHECK 2 : Is the adjustment request valid? -------------------- 
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

        // --------------- CHECK 3 : Calculate the new price ------------------
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

        // Manage all 3 checks, then the pricing-protection rule, then the two database writes.
        // Returns a result object rather than throwing for the expected "rejected" paths, so the
        // UI layer can branch on Success cleanly.
        public PriceAdjustmentResult ProcessPriceAdjustment(
            string productCode,
            string adjustmentType,
            decimal percentage,
            string reason,
            string approvedBy)
        {
            // 1. Confirm that the product is real/exists?
            if (!ValidateProductExists(productCode, out var product, out var errorMessage))
            { return PriceAdjustmentResult.Fail(errorMessage); } // stop immediately - nothing below runs

            // 2. Confirm the requested adjustment itself is valid?
            if (!ValidateAdjustment(percentage, adjustmentType, reason, approvedBy, out errorMessage))
            { return PriceAdjustmentResult.Fail(errorMessage); } // stop immediately 

            // 3. Only now can we calculate a new price.
            decimal newPrice = CalculateNewPrice(product.CurrentSellingPrice,
                adjustmentType, percentage);

            if (adjustmentType.ToUpper() == "MARKDOWN" && newPrice < product.MinimumSellingPrice)
            {
                return PriceAdjustmentResult.Fail(
                    "Price adjustment rejected. The requested markdown would reduce the " +
                    "selling price below the approved minimum selling price.");
            }

            try
            {
                decimal previousPrice = product.CurrentSellingPrice;

                _productRepository.UpdateSellingPrice(product.ProductId, newPrice);

                var record = new PriceAdjustment(
                    0,
                    product.ProductId, 
                    adjustmentType.ToUpper(),
                    percentage, 
                    previousPrice, 
                    newPrice,
                    DateTime.Now,
                    reason, 
                    approvedBy);

                _adjustmentRepository.Insert(record);

                product.UpdateSellingPrice(newPrice); // keep the in-memory object consistent
                return PriceAdjustmentResult.Ok(product, previousPrice, newPrice);
            }
            catch(System.Data.SqlClient.SqlException)
            {
                // Don't leak raw DB detail to the end user - log it instead
                // in a real system (e.g. File.AppendAllText("error.log", ...)).
                return PriceAdjustmentResult.Fail("A database error occurred. Please try again.");
            }
        }
        // PriceAdjustmentService ends here!!!!!
    }
}

// Simple result wrapper so the UI layer never has to catch exceptions for expected validation failures.
public class PriceAdjustmentResult
{
    public bool Success { get; private set; }
    public string ErrorMessage { get; private set; }
    public Product Product { get; private set; }
    public decimal PreviousPrice { get; private set; }
    public decimal NewPrice { get; private set; }

    public static PriceAdjustmentResult Ok(
        Product product,
        decimal previousPrice,
        decimal newPrice) =>
        new PriceAdjustmentResult
        {
            Success = true,
            Product = product,
            PreviousPrice = previousPrice,
            NewPrice = newPrice
        };

    public static PriceAdjustmentResult Fail(string errorMessage) =>
        new PriceAdjustmentResult { Success = false, ErrorMessage = errorMessage };
}

