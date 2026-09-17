using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Pricing
{
    public interface IPriceAdjustmentStrategy
    {
        decimal Apply(decimal currentPrice, decimal percentage);
        IPriceAdjustmentStrategy GetStrategy(string adjustmentType);
    }
}
