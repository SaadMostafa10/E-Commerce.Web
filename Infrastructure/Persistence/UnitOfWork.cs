using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Persistence.Data;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntiy, TKey> GetRepository<TEntiy, TKey>() where TEntiy : BaseEntity<TKey>
        {
            // Get Type Name
            var typeName = typeof(TEntiy).Name;
            // Dic<string ,Object> ==> string Key [Name Of Type] -- Object Object From Generic Repository
            //if (_repositories.ContainsKey(typeName))
            //  return (IGenericRepository<TEntiy> ,<TKey>) _repositories[typeName];
            
            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntiy, TKey>)value;
            else
            {
                // Create Object 
                var Repo = new GenericRepository<TEntiy, TKey>(_dbContext);
                // Store Object In Dic
                _repositories["typeName"] = Repo;
                // Return Object 
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
    }
}
