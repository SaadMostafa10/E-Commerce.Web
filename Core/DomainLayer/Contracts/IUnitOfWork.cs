using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntiy, TKey> GetRepository<TEntiy, TKey>() where TEntiy : BaseEntity<TKey>;
        Task<int> SaveChangesAsync();
         
    }
}
