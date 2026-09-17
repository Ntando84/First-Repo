using RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Entities;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM.Domain.Interfaces
{
    /*Interfaces keep the Service layer depending on a CONTRACT, not a concrete SQL
     * implementation. This makes it possible to write a FakeProductRepository for
     * unit testing later.
     */
    public interface IProductRepository
    {
        Product FindByCode(string productCode);
        Product FindById(int productId);
        void UpdateSellingPrice(int productId, decimal newPrice);
    }
}
