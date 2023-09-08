namespace BeerWholesaleManagementSystem.Core.Repositories;

public interface IRepositories<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
}
