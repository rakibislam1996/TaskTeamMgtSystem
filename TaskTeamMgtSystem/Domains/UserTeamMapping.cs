namespace TaskTeamMgtSystem.Domains
{
    public class UserTeamMapping : BaseEntity
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}
