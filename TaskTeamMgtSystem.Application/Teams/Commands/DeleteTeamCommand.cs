using MediatR;

namespace TaskTeamMgtSystem.Application.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}