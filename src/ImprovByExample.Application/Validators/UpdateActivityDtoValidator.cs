using FluentValidation;
using ImprovByExample.Application.Common.Models.DTOs;

namespace ImprovByExample.Application.Validators;

public class UpdateActivityDtoValidator : AbstractValidator<UpdateActivityDto>
{
    public UpdateActivityDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid activity ID is required.");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Activity name is required.")
            .MaximumLength(200).WithMessage("Activity name cannot exceed 200 characters.");
        
        RuleFor(x => x.ActivityTypeId)
            .GreaterThan(0).WithMessage("Valid activity type is required.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");
        
        RuleFor(x => x.Rules)
            .NotEmpty().WithMessage("Rules are required.")
            .MaximumLength(5000).WithMessage("Rules cannot exceed 5000 characters.");
        
        RuleFor(x => x.Script)
            .MaximumLength(5000).WithMessage("Script cannot exceed 5000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Script));
        
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.");
        
        RuleFor(x => x.MinPlayers)
            .GreaterThan(0).WithMessage("Minimum players must be greater than 0.")
            .When(x => x.MinPlayers.HasValue);
        
        RuleFor(x => x.MaxPlayers)
            .GreaterThanOrEqualTo(x => x.MinPlayers)
            .WithMessage("Maximum players must be greater than or equal to minimum players.")
            .When(x => x.MaxPlayers.HasValue && x.MinPlayers.HasValue);
        
        RuleFor(x => x.DurationMinutes)
            .GreaterThan(0).WithMessage("Duration must be greater than 0.")
            .When(x => x.DurationMinutes.HasValue);
    }
}
