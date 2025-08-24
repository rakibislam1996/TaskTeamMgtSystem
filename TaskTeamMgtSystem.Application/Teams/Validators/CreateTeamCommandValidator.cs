using FluentValidation;
using TaskTeamMgtSystem.Application.Teams.Commands;

namespace TaskTeamMgtSystem.Application.Teams.Validators
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }
}
