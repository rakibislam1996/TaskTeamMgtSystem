using MediatR;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Commands
{
    public class DeleteUserTeamMappingCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}