using MediatR;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}