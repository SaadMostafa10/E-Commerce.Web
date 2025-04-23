using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        // Create Query
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);

            if(specifications.OrderBy != null)
                query = query.OrderBy(specifications.OrderBy);

            if(specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);

            if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Count > 0)
            {
                // foreach(var exp in specifications.IncludeExpressions)               
                //     query = query.Include(exp);

                // Aggregate LINQ Operator
                query = specifications.IncludeExpressions.Aggregate(query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));

            }
            return query;
        }   
    }
}
