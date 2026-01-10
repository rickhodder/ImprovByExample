using Ardalis.Specification;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Specifications;

/// <summary>
/// Specification for counting activities with filters, without includes or pagination
/// </summary>
public class ActivitiesCountSpec : Specification<ImprovActivity>
{
    public ActivitiesCountSpec(ActivityFilterDto filter)
    {
        // Apply same filters as ActivitiesFilterSpec but without includes or pagination
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
    }
}
