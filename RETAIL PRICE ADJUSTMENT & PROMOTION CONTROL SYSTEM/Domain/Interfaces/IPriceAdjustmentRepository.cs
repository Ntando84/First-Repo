using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces
{
    public interface IPriceAdjustmentRepository
    {
        void Insert(PriceAdjustment adjustment);
        List<PriceAdjustment> GetHistoryForProduct(int productId);
    }
}
