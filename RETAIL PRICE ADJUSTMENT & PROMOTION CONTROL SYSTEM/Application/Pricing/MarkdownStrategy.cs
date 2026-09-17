namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Application.Pricing
{
    public class MarkdownStrategy
    {
        public decimal Apply(decimal currentPrice, decimal percentage) =>
            currentPrice - (currentPrice * percentage / 100);
    }
}
