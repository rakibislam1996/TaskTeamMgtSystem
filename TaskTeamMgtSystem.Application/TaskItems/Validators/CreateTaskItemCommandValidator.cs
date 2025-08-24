using FluentValidation;
using TaskTeamMgtSystem.Application.TaskItems.Commands;

namespace TaskTeamMgtSystem.Application.TaskItems.Validators
{
    public class CreateTaskItemCommandValidator : AbstractValidator<CreateTaskItemCommand>
    {
        public CreateTaskItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Task title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.AssignedToUserId)
                .GreaterThan(0).WithMessage("Assigned user ID must be greater than 0.");

            RuleFor(x => x.CreatedByUserId)
                .GreaterThan(0).WithMessage("Created by user ID must be greater than 0.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("Team ID must be greater than 0.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(BeValidStatus).WithMessage("Status must be one of: ToDO, InProgress, Done");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now).When(x => x.DueDate.HasValue)
                .WithMessage("Due date must be in the future.");
        }

        private bool BeValidStatus(string status)
        {
            return new[] { "ToDO", "InProgress", "Done" }.Contains(status);
        }
    }
}
