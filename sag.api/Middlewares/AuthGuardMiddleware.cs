using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using sag.Application.Exceptions;
using sag.Domain.Common.Enums;
using sag.Domain.Entities;

namespace sag.api.Middlewares;

public class AuthGuardMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly byte[] _secret;

    public AuthGuardMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _tokenHandler = new JwtSecurityTokenHandler();
        _secret = Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"] ?? string.Empty);
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers
            .FirstOrDefault(h => h.Key == "Authorization")
            .Value.ToString().Split(' ').Last();

        if (!string.IsNullOrEmpty(token))
            AttachUserToContext(context, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        // Step 1: Validate Token
        try
        {
            _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secret),
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var user = new User
            {
                Id = Guid.Parse(jwtToken.Claims.First(t => t.Type == "id").Value),
                Name = jwtToken.Claims.First(t => t.Type == "unique_name").Value,
                Email = jwtToken.Claims.First(t => t.Type == "email").Value,
                Status = EntityStatus.Enable,
            };

            context.Items.Add("User", user);
        }
        catch (Exception e)
        {
            context.Items.Add("Unauthorized", e.Message);
            // throw new UnauthorizedException(e.Message);
        }
        /*
         * ValidateIssuer = false,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["AppSettings:Secret"],
                ValidAudience = configuration["AppSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
         */
        // Step 2: Attach User
    }
}