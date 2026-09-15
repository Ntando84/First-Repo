using System;
using System.Collections.Generic;
using System.Text;

namespace RETAIL_PRICE_ADJUSTMENT___PROMOTION_CONTROL_SYSTEM
{
    public class ProductValidation : IProductValidation
    {
        private readonly IProductRepository _productRepository;

        public ProductValidation(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Step 1 : Search the repository for the product
        // Ask the repository (data access layer) to look the product up
        public Product GetProductByCode(string productCode)
        {
            return _productRepository.FindByCode(productCode);
        }

        // Step 2 : Decide whether processing can continue
        // 'out' parameters allow this one method to hand back BOTH
        // the product (if found) AND an error message (if not found)
        public bool ValidateProductExists(
            string productCode,
            out Product product,
            out string errorMessage)
        {
            product = GetProductByCode(productCode);
            
            if(product == null)
            {
                // No product was found for this code -> stop here.
                // Nothing below this point should ever run for an invalid code.
                errorMessage = $"Product with code '{productCode}' does not exist.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
