using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TaskTeamMgtSystem.Application.TaskItems.Commands;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Application.TaskItems.Handlers;

public class CreateTaskItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateTaskItem()
    {
        var options = new DbContextOptionsBuilder<TaskTeamMgtSystemDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateTaskItemTestDb")
            .Options;
        using var db = new TaskTeamMgtSystemDbContext(options);
        var handler = new CreateTaskItemCommandHandler(db);
        var command = new CreateTaskItemCommand
        {
            Title = "Test Task",
            Description = "Test Desc",
            AssignedToUserId = 1,
            CreatedByUserId = 2,
            TeamId = 1,
            Status = "ToDO",
            Priority = "High"
        };
        var result = await handler.Handle(command, CancellationToken.None);
        Assert.True(result > 0);
        Assert.Single(db.Tasks);
    }
}
