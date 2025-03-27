
namespace Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		readonly MainContext mainContext;
		readonly ConcurrentDictionary<string,object> storedRepositories;

		public UnitOfWork(MainContext mainContext, ConcurrentDictionary<string, object> repositories)
		{
			this.mainContext = mainContext;
			this.storedRepositories = repositories;
		}

		public async Task<int> SaveChangesAsync()
		=> await mainContext.SaveChangesAsync();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		=> (GenericRepository<TEntity, TKey>)
			storedRepositories.GetOrAdd(typeof(TEntity).Name
				, _ => new GenericRepository<TEntity, TKey>(mainContext));

		// الليله اللى فوق دى عشان لما اجى اكريت ابجكت مره يتخزن ولما اجى احتاجه تانى
		// استدعيه بدل م اكريته من اول وجديد ونملى الميمورى ع الفاضى
	}
}
