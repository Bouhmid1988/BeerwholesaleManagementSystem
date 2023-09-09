using BeerWholesaleManagementSystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeerWholesaleManagementSystem.Data.Repositories;

public class Repositories<TEntity>: IRepositories<TEntity> where TEntity : class
{
    private readonly DbContext _context;

    public Repositories(DbContext context)
    {
        _context = context;
    }
    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken) => await _context.Set<TEntity>().FindAsync(id, cancellationToken);

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken) => await _context.Set<TEntity>().ToListAsync(cancellationToken);

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity == null)
            return false;

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
