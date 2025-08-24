using MediatR;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Teams.Queries
{
    public class GetTeamByIdQuery : IRequest<Team>
    {
        public int Id { get; set; }
    }
}