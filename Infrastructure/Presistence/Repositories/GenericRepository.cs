
namespace Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		readonly MainContext mainContext;
		public GenericRepository(MainContext mainContext)
		{
			this.mainContext = mainContext;
		}

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
        
		// with specs
		public async Task<TEntity?> GetAsync(IBaseSpecifications<TEntity> specifications)
		=> await SpecificationEvaluator.CreateQuery<TEntity>(mainContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
		
		public async Task<IEnumerable<TEntity>> GetAllAsync(IBaseSpecifications<TEntity> specifications)
		=> await SpecificationEvaluator.CreateQuery<TEntity>(mainContext.Set<TEntity>(), specifications).ToListAsync();
		
		
	}
}
