using TaskTeamMgtSystem.Application.Common.Interfaces;
using TaskTeamMgtSystem.Core.Domain.Entities;

namespace TaskTeamMgtSystem.Application.Features.Teams.Queries
{
    public class GetTeamByIdQuery : IQuery<Team>
    {
        public int Id { get; set; }
    }
}