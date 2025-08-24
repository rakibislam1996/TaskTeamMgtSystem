using Xunit;
using Moq;
using TaskTeamMgtSystem.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public class AuthenticationServiceTests
{
    [Fact]
    public void Authenticate_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        var user = new User { PasswordHash = null };
        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Test123!");
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, "Test123!");
        Assert.Equal(PasswordVerificationResult.Success, result);
    }

    [Fact]
    public void Authenticate_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        var user = new User { PasswordHash = null };
        var passwordHasher = new PasswordHasher<User>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Test123!");
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, "WrongPassword");
        Assert.Equal(PasswordVerificationResult.Failed, result);
    }
}
