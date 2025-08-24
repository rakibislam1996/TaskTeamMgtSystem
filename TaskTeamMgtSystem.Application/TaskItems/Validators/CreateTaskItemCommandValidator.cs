using FluentValidation;
using TaskTeamMgtSystem.Application.TaskItems.Commands;

namespace TaskTeamMgtSystem.Application.TaskItems.Validators
{
    public class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommand>
    {
        public CreateTaskItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .MaximumLength(500);
            RuleFor(x => x.AssignedToUserId)
                .GreaterThan(0).WithMessage("AssignedToUserId must be greater than 0.");
            RuleFor(x => x.CreatedByUserId)
                .GreaterThan(0).WithMessage("CreatedByUserId must be greater than 0.");
            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");
            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required.");
        }
    }
}
