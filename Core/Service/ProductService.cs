using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceAbstraction;
using Services.Specifications;
using Shared;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork ,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTo>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTo>>(Brands);
            return BrandsDto;
        }

        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specifications = new ProductWithBrandAndTypeSpecification(queryParams);
            var Products = await Repo.GetAllAsync(specifications :Specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(Products);
            var ProductCount = Products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDTo> ( queryParams.PageIndex,ProductCount, TotalCount,Data );
        }

        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(Types);
            return TypesDto;
        }

        public async Task<ProductDTo> GetProductByIdAsync(int id )
        {
            var Specifications = new ProductWithBrandAndTypeSpecification(id);
            var Product = await _unitOfWork.GetRepository<Product , int>().GetByIdAsync(Specifications);
            return _mapper.Map<Product, ProductDTo>(Product);
        }
    }
}
