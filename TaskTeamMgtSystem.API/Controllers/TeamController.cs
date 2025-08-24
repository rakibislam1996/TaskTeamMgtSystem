using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.Teams.Commands;
using TaskTeamMgtSystem.Application.Teams.Queries;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetTeams() => Ok(await _mediator.Send(new GetTeamsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(int id) => Ok(await _mediator.Send(new GetTeamByIdQuery { Id = id }));

        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamCommand command) => Ok(await _mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(UpdateTeamCommand command) => Ok(await _mediator.Send(command));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id) => Ok(await _mediator.Send(new DeleteTeamCommand { Id = id }));
    }
}
