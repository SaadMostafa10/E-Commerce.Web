using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<IEnumerable<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams);
        // Get Product By Id
        Task<ProductDTo> GetProductByIdAsync(int Id);
        // Get All Types
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();
        // Get All Brands
        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();
    }
}
