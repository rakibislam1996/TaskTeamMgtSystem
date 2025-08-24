using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskTeamMgtSystem.Application.TaskItems.Queries;
using TaskTeamMgtSystem.Core.Domain.Entities;
using TaskTeamMgtSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Application.TaskItems.Handlers;

public class GetTaskItemsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldFilterTasksByStatus()
    {
        var options = new DbContextOptionsBuilder<TaskTeamMgtSystemDbContext>()
            .UseInMemoryDatabase(databaseName: "FilterTasksTestDb")
            .Options;
        using var db = new TaskTeamMgtSystemDbContext(options);
        db.Tasks.Add(new TaskItem { Title = "T1", Status = TaskTeamMgtSystem.Core.Domain.Enums.TasksStatus.ToDO, AssignedToUserId = 1, TeamId = 1 });
        db.Tasks.Add(new TaskItem { Title = "T2", Status = TaskTeamMgtSystem.Core.Domain.Enums.TasksStatus.Done, AssignedToUserId = 2, TeamId = 1 });
        db.SaveChanges();
        var handler = new GetTaskItemsQueryHandler(db);
        var query = new GetTaskItemsQuery { Status = "ToDO" };
        var result = await handler.Handle(query, CancellationToken.None);
        Assert.Single(result);
        Assert.Equal(TaskTeamMgtSystem.Core.Domain.Enums.TasksStatus.ToDO, result[0].Status);
    }
}
