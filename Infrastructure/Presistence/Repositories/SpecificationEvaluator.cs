
namespace Persistence.Repositories
{
	public static class SpecificationEvaluator
	{
		// method to create query with specs 
		public static IQueryable<TEntity> CreateQuery<TEntity>
			(IQueryable<TEntity> inQuery, IBaseSpecifications<TEntity> specifications)
			where TEntity : class
		{
			var Query = inQuery;
			// Query = mainContext.Set<T>();
			if (specifications.Criteria is not null)
				Query = Query.Where(specifications.Criteria);
			// Query = mainContext.Set<T>().Where(......);
			if (specifications.IncludeExpression is not null && specifications.IncludeExpression.Any())
				foreach (var item in specifications.IncludeExpression)
					Query.Include(item);
			// Query = mainContext.Set<T>().Where(.......).Include(......).Include(......);
			return Query;
		}
	}
}
