using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImprovByExample.Infrastructure.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class
{
    protected readonly ImprovDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public ReadRepository(ImprovDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }
}
