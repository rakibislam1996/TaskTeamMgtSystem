using FluentValidation;
using TaskTeamMgtSystem.Application.Features.Users.Commands;

namespace TaskTeamMgtSystem.Application.Features.Users.Validators
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().MinimumLength(6);
        }
    }
}
