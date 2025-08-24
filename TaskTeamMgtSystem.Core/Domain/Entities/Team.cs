using System.Collections.Generic;
using TaskTeamMgtSystem.Core.Domain.Common;

namespace TaskTeamMgtSystem.Core.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public virtual ICollection<TaskItem> Tasks { get; set; }
        public virtual ICollection<UserTeamMapping> TeamMembers { get; set; }

        public Team()
        {
            Tasks = new HashSet<TaskItem>();
            TeamMembers = new HashSet<UserTeamMapping>();
        }
    }
}