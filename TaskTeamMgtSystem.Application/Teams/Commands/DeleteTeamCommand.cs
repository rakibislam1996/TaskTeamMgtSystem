using MediatR;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}