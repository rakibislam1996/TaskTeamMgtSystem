using MediatR;

namespace TaskTeamMgtSystem.Application.UserTeamMappings.Commands
{
    public class DeleteUserTeamMappingCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}