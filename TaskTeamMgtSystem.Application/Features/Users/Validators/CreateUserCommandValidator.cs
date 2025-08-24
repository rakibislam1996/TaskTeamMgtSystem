using FluentValidation;
using TaskTeamMgtSystem.Application.Features.Users.Commands;

namespace TaskTeamMgtSystem.Application.Features.Users.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Role)
                .NotEmpty().MaximumLength(50);
            RuleFor(x => x.Password)
                .NotEmpty().MinimumLength(6);
        }
    }
}
