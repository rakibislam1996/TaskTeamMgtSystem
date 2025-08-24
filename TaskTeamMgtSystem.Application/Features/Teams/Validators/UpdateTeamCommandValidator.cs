using FluentValidation;
using TaskTeamMgtSystem.Application.Features.Teams.Commands;

namespace TaskTeamMgtSystem.Application.Features.Teams.Validators
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(250);
        }
    }
}
