using Xunit;
using MediatR;
using TaskTeamMgtSystem.Application.Users.Commands;
using TaskTeamMgtSystem.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskTeamMgtSystem.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTeamMgtSystem.Tests.UserTests
{
    public class CreateUserCommandHandlerTests
    {
        private readonly TaskTeamMgtSystemDbContext _context;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<TaskTeamMgtSystemDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid()));

            var serviceProvider = services.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<TaskTeamMgtSystemDbContext>();
            _handler = new CreateUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateUser()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Employee",
                Password = "Test123!"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result > 0);
            var createdUser = await _context.Users.FindAsync(result);
            Assert.NotNull(createdUser);
            Assert.Equal(command.FullName, createdUser.FullName);
            Assert.Equal(command.Email, createdUser.Email);
            Assert.Equal(command.Role, createdUser.Role);
            Assert.NotNull(createdUser.PasswordHash);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldHashPassword()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Employee",
                Password = "Test123!"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var createdUser = await _context.Users.FindAsync(result);
            Assert.NotNull(createdUser.PasswordHash);
            Assert.NotEqual(command.Password, createdUser.PasswordHash);
            Assert.True(BCrypt.Net.BCrypt.Verify(command.Password, createdUser.PasswordHash));
        }
    }
}
