using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public interface IProductValidation
    {
        bool ValidateProductExists(
            string productCode, 
            out Product product, 
            out string errorMessage);
    }
}
