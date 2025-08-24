using FluentValidation;
using TaskTeamMgtSystem.Application.UserTeamMappings.Commands;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Validators
{
    public class CreateUserTeamMappingCommandValidator : AbstractValidator<CreateUserTeamMappingCommand>
    {
        public CreateUserTeamMappingCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("User ID must be greater than 0.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("Team ID must be greater than 0.");
        }
    }
}
