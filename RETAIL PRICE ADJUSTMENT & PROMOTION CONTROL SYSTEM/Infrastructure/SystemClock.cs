using System;
using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Infrastructure
{
    public class SystemClock : IClock
    {
        // The one concrete place that actually calls DateTime.Now
        public DateTime Now => DateTime.Now;
    }
}
