using MediatR;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}