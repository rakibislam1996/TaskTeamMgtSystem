using MediatR;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class UpdateTeamCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}