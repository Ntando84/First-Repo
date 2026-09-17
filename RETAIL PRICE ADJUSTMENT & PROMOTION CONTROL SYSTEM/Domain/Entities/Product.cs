using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities
{
    public class Product
    {
        /*Represents a single retail product.
         * Mirrors the Product table exactly.
         * CurrentSellingPrice can be READ from anywhere, but can only be CHANGED by
         * calling UpdateSellingPrice(), never by direct assignment.
         */
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MinimumSellingPrice { get; set; }

        // CurrentSellingPrice set to private so outside code cannot change it,
        // only changed through the UpdateSellingPrice method below.
        public decimal CurrentSellingPrice { get; private set; }

        // Optional navigation property to Category.
        // Populated when a query joins to Category, left null otherwise.
        public Category Category { get; set; }

        public Product() { }

        public Product(int productId, string productCode,
            string productName, int categoryId, decimal unitCost,
            decimal currentSellingPrice, decimal minimumSellingPrice)
        {
            ProductId = productId;
            ProductCode = productCode; 
            ProductName = productName; 
            CategoryId = categoryId;
            UnitCost = unitCost;
            CurrentSellingPrice = currentSellingPrice;
            MinimumSellingPrice = minimumSellingPrice;
        }

        // The only way CurrentSellingPrice is allowed to change.
        public void UpdateSellingPrice(decimal newPrice)
        { 
            if (newPrice < 0)
            {
                throw new ArgumentException("Selling price must be greater than zero.");
            }

            CurrentSellingPrice = newPrice; 
        }

        public override string ToString() =>
            $"{ProductCode} - {ProductName} (R{CurrentSellingPrice:F2})";
    }
}
