using MediatR;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}