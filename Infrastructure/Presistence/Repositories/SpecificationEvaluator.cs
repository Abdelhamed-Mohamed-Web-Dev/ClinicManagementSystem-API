
namespace Persistence.Repositories
{
    // ABDELRAHMAN
    internal static class SpecificationEvaluator
    {
        public  static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQuery,
            Specifications<T> specifications) 
             where T: class
            
        {
            var query = inputQuery;
            if(specifications.Criteria is not null) query= query.Where(specifications.Criteria);


            query = specifications.IncludeExpressions.Aggregate(query, (CurrentQuery, includeExpression) => CurrentQuery.Include(includeExpression));

            return query;
            
            }
    }
}
