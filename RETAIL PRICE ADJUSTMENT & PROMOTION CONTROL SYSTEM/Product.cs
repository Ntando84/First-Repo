using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MinimumSellingPrice { get; set; }

        // CurrentSellingPrice set to private so outside code cannot change it,
        // only changed through the UpdateSellingPrice method below.
        public decimal CurrentSellingPrice { get; private set; }

        public void UpdateSellingPrice(decimal newPrice) 
            => CurrentSellingPrice = newPrice;

    }
}
