using System;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Models;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Repositories;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Services
{
    public interface IPriceAdjustmentService
    {
        bool ValidateAdjustment(decimal percentage, string adjustmentType,
            string reason, string approvedBy, out string error);

    }
}
