using System;
using System.Collections.Generic;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Pricing
{
    public class PriceAdjustmentStrategyFactory
    {
        private readonly Dictionary<string, IPriceAdjustmentStrategy> _strategies =
            new Dictionary<string, IPriceAdjustmentStrategy>(StringComparer.OrdinalIgnoreCase)
            {
                { "MARKUP", new MarkupStrategy() },
                { "MARKDOWN", new MarkdownStrategy() }
            };

        public IPriceAdjustmentStrategy GetStrategy(string adjustmentType)
        {
            if (!_strategies.TryGetValue(adjustmentType ??
                string.Empty, out var strategy))
                throw new ArgumentException($"No pricing strategy registered for '{adjustmentType}'.");
            return strategy;
        }
    }
}
