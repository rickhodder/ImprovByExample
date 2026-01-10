using Ardalis.Specification.EntityFrameworkCore;
using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Infrastructure.Data;

namespace ImprovByExample.Infrastructure.Repositories;

public class Repository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public Repository(ImprovDbContext dbContext) : base(dbContext)
    {
    }
}
