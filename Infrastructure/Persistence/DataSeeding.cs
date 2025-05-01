using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
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
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMirations = await _dbContext.Database.GetPendingMigrationsAsync();
                // Production
                if (PendingMirations.Any())
                {
                   await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    // Read Data
                    // var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    // Convert Data "String" => C# Object [ProductBrand]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    // Save To DB
                    if (ProductBrands is not null && ProductBrands.Any())
                       await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_dbContext.Set<ProductType>().Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    //Convert Data "String" => C# Object [ProductBrand]
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                       await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);

                }
                await _dbContext.SaveChangesAsync();
                if (!_dbContext.Set<Product>().Any())
                { 
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    //Convert Data "String" => C# Object [ProductBrand]
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // TODO
            }
            


        }
    }
}
