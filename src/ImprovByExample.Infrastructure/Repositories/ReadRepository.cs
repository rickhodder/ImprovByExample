using Ardalis.Specification.EntityFrameworkCore;
using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Infrastructure.Data;

namespace ImprovByExample.Infrastructure.Repositories;

public class ReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
{
    public ReadRepository(ImprovDbContext dbContext) : base(dbContext)
    {
    }
}
