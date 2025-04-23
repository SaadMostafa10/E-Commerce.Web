using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecification :BaseSpecifications<Product ,int>
    {
        // Get All Products With Types And Brands
        public ProductWithBrandAndTypeSpecification(int?BrandId ,int?TypeId ,ProductSortingOptions sortingOption)
            :base(P=>(!BrandId.HasValue || P.BrandId ==BrandId) 
            && (!TypeId.HasValue || P.TypeId==TypeId))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

            switch (sortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;





            }

        }
        // Get Product By Id 
        public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id ==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
