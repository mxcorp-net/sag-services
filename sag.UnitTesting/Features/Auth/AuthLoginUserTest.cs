using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using sag.Application.Features.Auth.Handlers;
using sag.Application.Features.Auth.Models;
using sag.Application.Features.Auth.Queries;
using sag.Persistence.Contexts;
using sag.UnitTesting.Commons;

namespace sag.UnitTesting.Features.Auth;

public class AuthLoginUserTest
{
    private readonly SagDbContext _dbContext = new InMemorySagDbContext().CreateContext();
    private readonly IConfiguration _configuration = new ConfigurationManager();

    [Theory]
    [InlineData("x", "y")]
    [InlineData("test@email.com", "y")]
    public async Task LoginUserTest_Exceptions(string email, string password)
    {
        var handler = new LoginUserHandler(_dbContext, _configuration);
        var loginRequest = new LoginRequest
        {
            Email = email,
            Password = password
        };

        var action = async () => await handler.Handle(new LoginUserQuery(loginRequest), CancellationToken.None);

        await action.Should().ThrowAsync<BadHttpRequestException>();
    }
    
    //TODO: LoginUserTest Success
}