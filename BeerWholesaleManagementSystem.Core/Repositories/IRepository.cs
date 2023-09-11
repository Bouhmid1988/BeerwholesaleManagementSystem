namespace BeerWholesaleManagementSystem.Core.Repositories;

/// <summary>
/// Define the Entity Repository Interface
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Get an entity by its identifier asynchronously
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Get All entity asynchronously
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Add an new entity  asynchronously
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operatio</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update an  entity  asynchronously
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete an  entity  asynchronously
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The task that represents the asynchronous operation</returns>
    Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken);
}
