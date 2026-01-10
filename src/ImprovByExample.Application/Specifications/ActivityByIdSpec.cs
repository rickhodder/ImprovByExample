using Ardalis.Specification;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Specifications;

public class ActivityByIdSpec : Specification<ImprovActivity>, ISingleResultSpecification<ImprovActivity>
{
    public ActivityByIdSpec(int id)
    {
        Query.Where(a => a.Id == id)
             .Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include(a => a.Difficulty)
             .Include(a => a.CreatedBy);
    }
}
