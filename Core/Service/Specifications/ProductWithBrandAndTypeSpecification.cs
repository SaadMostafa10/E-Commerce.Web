using DomainLayer.Models;
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
        public ProductWithBrandAndTypeSpecification():base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
        // Get Product By Id 
        public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id ==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);

        }
    }
}
