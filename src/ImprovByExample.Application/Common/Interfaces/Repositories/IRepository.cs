using Ardalis.Specification;

namespace ImprovByExample.Application.Common.Interfaces.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
