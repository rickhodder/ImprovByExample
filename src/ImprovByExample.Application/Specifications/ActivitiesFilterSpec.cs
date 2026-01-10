using Ardalis.Specification;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Specifications;

public class ActivitiesFilterSpec : Specification<ImprovActivity>
{
    public ActivitiesFilterSpec(ActivityFilterDto filter)
    {
        Query.Include(a => a.ActivityType)
             .Include(a => a.ActivitySource)
             .Include(a => a.Difficulty);
        
        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            Query.Where(a => 
                a.Name.Contains(filter.SearchTerm) || 
                a.Description.Contains(filter.SearchTerm) ||
                a.Category.Contains(filter.SearchTerm));
        }
        
        if (filter.ActivityTypeId.HasValue)
        {
            Query.Where(a => a.ActivityTypeId == filter.ActivityTypeId.Value);
        }
        
        if (filter.ActivitySourceId.HasValue)
        {
            Query.Where(a => a.ActivitySourceId == filter.ActivitySourceId.Value);
        }
        
        if (filter.DifficultyId.HasValue)
        {
            Query.Where(a => a.DifficultyId == filter.DifficultyId.Value);
        }
        
        if (filter.MinPlayers.HasValue)
        {
            Query.Where(a => !a.MinPlayers.HasValue || a.MinPlayers <= filter.MinPlayers.Value);
        }
        
        if (filter.MaxPlayers.HasValue)
        {
            Query.Where(a => !a.MaxPlayers.HasValue || a.MaxPlayers >= filter.MaxPlayers.Value);
        }
        
        // Pagination
        if (filter.IsPaged)
        {
            Query.Skip(filter.PageIndex * filter.PageSize)
                 .Take(filter.PageSize);
        }
        
        Query.OrderBy(a => a.Name);
    }
}
