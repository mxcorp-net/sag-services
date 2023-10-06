using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using sag.Application.Common.Structs;
using sag.Application.Exceptions;
using sag.Application.Features.Auth.Queries;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Auth.Handlers;

public class LoginUserHandler : IRequestHandler<LoginUserQuery, Response<object>>
{
    private readonly SagDbContext _dbContext;
    private readonly IConfiguration _config;

    public LoginUserHandler(SagDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _config = configuration;
    }

    public async Task<Response<object>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u =>
            u.Email == request.LoginData.Email, cancellationToken: cancellationToken);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.LoginData.Password, user.Password))
            throw new BadRequestException("Email or Password are invalid!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["AppSettings:Secret"] ?? Guid.NewGuid().ToString());
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("id", user.Id.ToString()),
                new Claim("permissions", "Hola Mundo!"),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return Response<object>.Success(new
        {
            Token = tokenHandler.WriteToken(token)
        });
    }
}