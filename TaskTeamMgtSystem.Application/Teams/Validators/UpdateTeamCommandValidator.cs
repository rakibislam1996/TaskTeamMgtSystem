using FluentValidation;
using TaskTeamMgtSystem.Application.Teams.Commands;

namespace TaskTeamMgtSystem.Application.Teams.Validators
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Team ID must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(100).WithMessage("Team name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
