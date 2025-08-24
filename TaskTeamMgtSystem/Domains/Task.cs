namespace TaskTeamMgtSystem.Domains
{
    public class Task : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; } // e.g., Pending, In Progress, Completed
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
