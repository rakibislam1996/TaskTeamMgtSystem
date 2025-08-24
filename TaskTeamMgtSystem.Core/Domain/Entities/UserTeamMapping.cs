using TaskTeamMgtSystem.Core.Domain.Common;

namespace TaskTeamMgtSystem.Core.Domain.Entities
{
    public class UserTeamMapping : BaseEntity
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
}