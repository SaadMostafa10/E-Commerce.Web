using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Options;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTo>()
                .ForMember(dist =>dist.BrandName ,Options=>Options.MapFrom(Src=>Src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, Options => Options.MapFrom(Src => Src.ProductType.Name));
            CreateMap<ProductType, TypeDTo>();
            CreateMap<ProductBrand, TypeDTo>();
        
        }
    }
}
