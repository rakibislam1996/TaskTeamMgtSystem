using FluentValidation;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;

namespace TaskTeamMgtSystem.Application.Features.Teams.Validators
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }
}
