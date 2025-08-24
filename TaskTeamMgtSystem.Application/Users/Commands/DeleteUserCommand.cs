using MediatR;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}