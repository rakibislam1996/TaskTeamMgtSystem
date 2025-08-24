using System;
using TaskTeamMgtSystem.Core.Domain.Common;
using TaskTeamMgtSystem.Core.Domain.Enums;

namespace TaskTeamMgtSystem.Core.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }

        // Navigation properties
        public virtual User AssignedTo { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Team Team { get; set; }
    }
}