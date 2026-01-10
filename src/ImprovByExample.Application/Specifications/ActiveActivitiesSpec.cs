using Ardalis.Specification;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Specifications;

public class ActiveActivitiesSpec : Specification<ImprovActivity>
{
    public ActiveActivitiesSpec()
    {
        Query.Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include(a => a.Difficulty)
             .Include(a => a.CreatedBy)
             .OrderBy(a => a.Name);
    }
}
