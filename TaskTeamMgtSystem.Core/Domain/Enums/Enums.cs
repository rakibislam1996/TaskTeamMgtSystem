namespace TaskTeamMgtSystem.Core.Domain.Enums
{
    public enum TaskStatus
    {
        Pending,
        InProgress,
        UnderReview,
        Completed,
        OnHold,
        Cancelled
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum UserRole
    {
        Admin,
        Manager,
        Employee
    }
}