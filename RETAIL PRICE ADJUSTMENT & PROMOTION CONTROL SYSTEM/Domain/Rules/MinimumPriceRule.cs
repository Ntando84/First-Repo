using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Rules
{
    /*Pure domain rule: given a Product and a calculated price, decide whether that price 
     * is allowed. It needs nothing but the Product entity and two primitives - no repository,
     * no database, no other layer.
     */
    public static class MinimumPriceRule
    {
        public static bool IsViolated(Product product, 
            string adjustmentType, decimal calculatedNewPrice)
        {
            return adjustmentType.ToUpper() == "MARKDOWN"
                && calculatedNewPrice < product.MinimumSellingPrice;
        }
    }
}
