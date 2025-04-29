using Domain.Contracts.ISpecifications;
using Domain.Entities;

namespace Domain.Contracts.IRepositories
{
	public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Task<TEntity> GetAsync(TKey Id);
		public Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false);
		public Task AddAsync(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
		// with specs
		public Task<TEntity> GetAsync(IBaseSpecifications<TEntity> specifications);
		public Task<IEnumerable<TEntity>> GetAllAsync(IBaseSpecifications<TEntity> specifications);

	}
}
