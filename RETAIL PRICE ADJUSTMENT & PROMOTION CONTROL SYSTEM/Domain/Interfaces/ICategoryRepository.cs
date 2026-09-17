using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Category FindById(int categoryId);
    }
}
