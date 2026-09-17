using System;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Repositories;
using System.Collections.Generic;
using System.Text;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Models;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Services
{
    public interface IProductService
    {
        bool ValidateProductExists(
            string productCode, 
            out Product product, 
            out string errorMessage);
    }
}
