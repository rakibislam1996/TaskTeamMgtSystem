using MediatR;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Commands
{
    public class CreateUserTeamMappingCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
    }
}