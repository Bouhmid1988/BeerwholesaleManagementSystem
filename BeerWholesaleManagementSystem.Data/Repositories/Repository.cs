using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories
{
    /// <summary>
    /// Generic repository class for performing common database operations.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to operate on.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc cref="IRepository{TEntity}.GetByIdAsync(int, CancellationToken)"/>
        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Set<TEntity>().FindAsync(id, cancellationToken);

        /// <inheritdoc cref="IRepository{TEntity}.GetAllAsync(CancellationToken)"/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) =>
            await _context.Set<TEntity>().ToListAsync(cancellationToken);

        /// <inheritdoc cref="IRepository{TEntity}.AddAsync(TEntity, CancellationToken)"/>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc cref="IRepository{TEntity}.UpdateAsync(TEntity, CancellationToken)"/>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc cref="IRepository{TEntity}.RemoveAsync(TEntity, CancellationToken)"/>
        public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity == null)
                return false;

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
