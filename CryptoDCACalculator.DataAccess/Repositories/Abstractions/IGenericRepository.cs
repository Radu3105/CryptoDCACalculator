using CryptoDCACalculator.DataAccess.Entities.Abstractions;

namespace LearningPlatform.DataAccess.Repositories.Abstractions
{
    public interface IGenericRepository<TEntity> where TEntity : GenericEntity
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}