using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities
{
    public class Category
    {
        /* Represents a product category/department (Electronics, Homeware, etc.)
         * Mirrors the Category table exactly.
         */
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public override string ToString() => CategoryName;
    }
}
