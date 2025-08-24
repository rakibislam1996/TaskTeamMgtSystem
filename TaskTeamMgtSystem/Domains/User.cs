namespace TaskTeamMgtSystem.Domains
{
    public class User : BaseEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; } // e.g., Admin, Manager, Employee
    }
}
