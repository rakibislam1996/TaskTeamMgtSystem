using Xunit;
using TaskTeamMgtSystem.Application.Users.Commands;
using TaskTeamMgtSystem.Application.Users.Validators;

public class CreateUserCommandValidatorTests
{
    [Fact]
    public void Should_Fail_When_Email_Is_Invalid()
    {
        var validator = new CreateUserCommandValidator();
        var command = new CreateUserCommand { FullName = "Test", Email = "invalid", Role = "Admin", Password = "Test123!" };
        var result = validator.Validate(command);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void Should_Pass_When_All_Fields_Are_Valid()
    {
        var validator = new CreateUserCommandValidator();
        var command = new CreateUserCommand { FullName = "Test", Email = "test@demo.com", Role = "Admin", Password = "Test123!" };
        var result = validator.Validate(command);
        Assert.True(result.IsValid);
    }
}
