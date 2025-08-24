using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.TaskItems.Commands;
using TaskTeamMgtSystem.Application.TaskItems.Queries;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskItemController(IMediator mediator) => _mediator = mediator;

        // Manager: create/update tasks
        [HttpPost]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> CreateTask(CreateTaskItemCommand command) => Ok(await _mediator.Send(command));

        [HttpPut]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> UpdateTask(UpdateTaskItemCommand command) => Ok(await _mediator.Send(command));

        // Employee: view/update only their own assigned tasks
        [HttpGet]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTasks([FromQuery] GetTaskItemsQuery query) => Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTask(int id) => Ok(await _mediator.Send(new GetTaskItemByIdQuery { Id = id }));

        [HttpPut("employee/update")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> UpdateOwnTask(UpdateTaskItemCommand command) => Ok(await _mediator.Send(command));

        // Manager: delete tasks
        [HttpDelete("{id}")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> DeleteTask(int id) => Ok(await _mediator.Send(new DeleteTaskItemCommand { Id = id }));
    }
}
