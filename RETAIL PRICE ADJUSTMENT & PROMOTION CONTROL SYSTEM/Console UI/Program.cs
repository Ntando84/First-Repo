using System;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Repositories;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Services;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new PriceAdjustmentService(
                new ProductRepository(),
                new PriceAdjustmentRepository());

            Console.WriteLine("=== Retail Price Adjustment & Promotion Control System ===\n");

            while (true)
            {
                Console.Write("Enter ProductCode (or 'exit' to quit): ");
                string productCode = Console.ReadLine();
                if (string.Equals(productCode, "exit", StringComparison.OrdinalIgnoreCase))
                    break;

                Console.Write("Enter AdjustmentType (MARKUP/MARKDOWN): ");
                string adjustmentType = Console.ReadLine();

                Console.Write("Enter AdjustmentPercentage: ");
                string percentageInput = Console.ReadLine();

                Console.Write("Enter Reason: ");
                string reason = Console.ReadLine();

                Console.Write("Enter ApprovedBy: ");
                string approvedBy = Console.ReadLine();

                if (!decimal.TryParse(percentageInput, out decimal percentage))
                {
                    Console.WriteLine(">> AdjustmentPercentage must be a valid number.\n");
                    continue; // caught here so a bad number never crashes the app
                }

                var result = service.ProcessPriceAdjustment(
                    productCode, adjustmentType, percentage, reason, approvedBy);

                if (result.Success)
                {
                    Console.WriteLine(
                        $">> Price adjustment processed successfully. " +
                        $"{result.Product.ProductCode}: R{result.PreviousPrice:F2} -> R{result.NewPrice:F2}\n");
                }
                else
                {
                    Console.WriteLine($">> {result.ErrorMessage}\n");
                }
            }

            Console.WriteLine("Goodbye.");
        }
    }
}
