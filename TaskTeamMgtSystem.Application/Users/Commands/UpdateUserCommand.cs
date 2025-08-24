using MediatR;

namespace TaskTeamMgtSystem.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}