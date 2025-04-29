using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public async Task<TEntity> GetAsync(Specifications<TEntity> specifications)
		  => await ApplySpecification(specifications).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
            => await ApplySpecification(specifications).ToListAsync();
		private IQueryable<TEntity> ApplySpecification(Specifications<TEntity> specifications) => SpecificationEvaluator.GetQuery<TEntity>(mainContext.Set<TEntity>(), specifications);

    }
}
