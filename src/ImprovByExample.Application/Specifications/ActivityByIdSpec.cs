using Ardalis.Specification;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Specifications;

public class ActivityByIdSpec : Specification<ImprovActivity>, ISingleResultSpecification<ImprovActivity>
{
    public ActivityByIdSpec(int activityId)
    {
        Query.Where(a => a.Id == activityId)
             .Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include(a => a.Difficulty);
    }
}
