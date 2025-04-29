
namespace Service.Specifications
{
	public class BaseSpecifications<TEntity> : IBaseSpecifications<TEntity> where TEntity : class
	{
		// props
		public Expression<Func<TEntity, bool>>? Criteria { get; }
		public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];
		// ctor to assign criteria "where"
		protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria) => Criteria = criteria;
		// method to add include to include list
		public void AddInclude(Expression<Func<TEntity, object>> Ex) => IncludeExpression.Add(Ex);

	}
}
