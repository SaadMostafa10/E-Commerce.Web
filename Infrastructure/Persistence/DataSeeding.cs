using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    //Convert Data "String" => C# Object [ProductBrand]
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands != null && ProductBrands.Any())
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    //Convert Data "String" => C# Object [ProductBrand]
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductTypes != null && ProductTypes.Any())
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    //Convert Data "String" => C# Object [ProductBrand]
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                        _dbContext.Products.AddRange(Products);
                }
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                // TODO
            }
            


        }
    }
}
