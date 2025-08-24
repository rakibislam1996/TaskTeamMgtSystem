using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTeamMgtSystem.Application.TaskItems.Commands;
using TaskTeamMgtSystem.Application.TaskItems.Queries;
using Microsoft.Extensions.Logging;

namespace TaskTeamMgtSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TaskItemController> _logger;
        public TaskItemController(IMediator mediator, ILogger<TaskItemController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // Manager: create/update tasks
        [HttpPost]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> CreateTask(CreateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task created: {@Command}", command);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> UpdateTask(UpdateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Task updated: {@Command}", command);
            return Ok(result);
        }

        // Employee: view/update only their own assigned tasks
        [HttpGet]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTasks([FromQuery] GetTaskItemsQuery query) => Ok(await _mediator.Send(query));

        [HttpGet("{id}")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> GetTask(int id) => Ok(await _mediator.Send(new GetTaskItemByIdQuery { Id = id }));

        [HttpPut("employee/update")]
        [Authorize(Policy = "Employee")]
        public async Task<IActionResult> UpdateOwnTask(UpdateTaskItemCommand command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Employee updated own task: {@Command}", command);
            return Ok(result);
        }

        // Manager: delete tasks
        [HttpDelete("{id}")]
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result = await _mediator.Send(new DeleteTaskItemCommand { Id = id });
            _logger.LogInformation("Task deleted: {TaskId}", id);
            return Ok(result);
        }
    }
}
