using System;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces
{
    // Abstracts 'Now' so tests can supply a fixed time instead of the real system clock.
    public class IClock
    {
        DateTime Now { get; }
    }
}
