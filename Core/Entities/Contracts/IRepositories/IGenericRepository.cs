using Domain.Contracts.ISpecifications;
using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Contracts.IRepositories
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Task<TEntity> GetAsync(TKey Id);
		public Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges =false);
		public Task<TEntity> GetAsync(Specifications<TEntity> specifications);
		public Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications);
		public Task AddAsync(TEntity entity);
		public void Update(TEntity entity);
		public void Delete(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        //// with specs
        //public Task<TEntity> GetAsync(IBaseSpecifications<TEntity> specifications);
        //public Task<IEnumerable<TEntity>> GetAllAsync(IBaseSpecifications<TEntity> specifications);

    }
}
