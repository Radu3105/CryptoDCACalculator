using CryptoDCACalculator.DataAccess.Context;
using CryptoDCACalculator.DataAccess.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LearningPlatform.DataAccess.Repositories.Abstractions
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : GenericEntity
    {
        protected readonly CryptoDCACalculatorContext context;
        private readonly DbSet<TEntity> _contextSet;

        public GenericRepository(CryptoDCACalculatorContext context)
        {
            this.context = context;
            _contextSet = this.context.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _contextSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _contextSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _contextSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _contextSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entityToDelete = await GetByIdAsync(id);
            _contextSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }
    }
}
