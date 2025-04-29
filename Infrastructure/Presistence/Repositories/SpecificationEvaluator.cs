using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public  static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQuery,
            Specifications<T> specifications) 
             where T: class
            
        {
            var query = inputQuery;
            if(specifications.Creiteria is not null) query= query.Where(specifications.Creiteria);


            query = specifications.IncludeExperessions.Aggregate(query, (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));

            return query;
            
            }
    }
}
