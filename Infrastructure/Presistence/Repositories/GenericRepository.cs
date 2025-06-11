
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		readonly MainContext mainContext;
		public GenericRepository(MainContext mainContext)
		{
			this.mainContext = mainContext;
		}

		#region Without Specifications
		public async Task AddAsync(TEntity entity)
		=> await mainContext.Set<TEntity>().AddAsync(entity);

		public void Update(TEntity entity)
		=> mainContext.Set<TEntity>().Update(entity);

		public void Delete(TEntity entity)
		=> mainContext.Set<TEntity>().Remove(entity);

		public async Task<TEntity?> GetAsync(TKey Id)
		=> await mainContext.Set<TEntity>().FindAsync(Id);

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false)
		=> TrackChanges ? await mainContext.Set<TEntity>().ToListAsync() :
			await mainContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => await mainContext.Set<TEntity>().AnyAsync(predicate);
        

        #endregion

        #region With Specifications
        public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
			=> await ApplySpecification(specifications).FirstOrDefaultAsync();

		public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
			=> await ApplySpecification(specifications).ToListAsync();

		private IQueryable<TEntity> ApplySpecification(Specifications<TEntity> specifications)
			=> SpecificationEvaluator.GetQuery<TEntity>(mainContext.Set<TEntity>(), specifications);

       
        #endregion

    }
}
