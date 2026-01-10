using Ardalis.Specification;

namespace ImprovByExample.Application.Common.Interfaces.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
