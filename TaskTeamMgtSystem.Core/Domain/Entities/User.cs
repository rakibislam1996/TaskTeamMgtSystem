using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Common;

namespace TaskTeamMgtSystem.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin, Manager, Employee

        // Navigation properties
        public virtual ICollection<Task> AssignedTasks { get; set; }
        public virtual ICollection<Task> CreatedTasks { get; set; }
        public virtual ICollection<UserTeamMapping> TeamMappings { get; set; }

        public User()
        {
            AssignedTasks = new HashSet<Task>();
            CreatedTasks = new HashSet<Task>();
            TeamMappings = new HashSet<UserTeamMapping>();
        }
    }
}