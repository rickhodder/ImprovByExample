using FluentValidation;
using ImprovByExample.Application.Common.Models.DTOs;

namespace ImprovByExample.Application.Validators;

public class ActivityFilterDtoValidator : AbstractValidator<ActivityFilterDto>
{
    public ActivityFilterDtoValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");
        
        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
        
        RuleFor(x => x.ActivityTypeId)
            .GreaterThan(0).WithMessage("Activity type ID must be greater than 0.")
            .When(x => x.ActivityTypeId.HasValue);
        
        RuleFor(x => x.ActivitySourceId)
            .GreaterThan(0).WithMessage("Activity source ID must be greater than 0.")
            .When(x => x.ActivitySourceId.HasValue);
        
        RuleFor(x => x.DifficultyId)
            .GreaterThan(0).WithMessage("Difficulty ID must be greater than 0.")
            .When(x => x.DifficultyId.HasValue);
        
        RuleFor(x => x.MinPlayers)
            .GreaterThan(0).WithMessage("Minimum players must be greater than 0.")
            .When(x => x.MinPlayers.HasValue);
        
        RuleFor(x => x.MaxPlayers)
            .GreaterThan(0).WithMessage("Maximum players must be greater than 0.")
            .When(x => x.MaxPlayers.HasValue);
    }
}
