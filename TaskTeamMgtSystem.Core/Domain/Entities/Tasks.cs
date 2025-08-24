using TaskTeamMgtSystem.Core.Domain.Common;
using System;

namespace TaskTeamMgtSystem.Core.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskTeamMgtSystem.Core.Domain.Enums.TasksStatus Status { get; set; }
        public string Priority { get; set; }
        // Navigation properties
        public virtual Team Team { get; set; }
        public virtual User AssignedToUser { get; set; }
        public virtual User CreatedByUser { get; set; }
    }
}
