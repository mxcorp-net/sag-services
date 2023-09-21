using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace sag.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddAuthGuard(configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IMediator, Mediator>();
        /*.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>()
        .AddTransient<IDateTimeService, DateTimeService>()
        .AddTransient<IEmailService, EmailService>();*/
    }
    
    private static void AddAuthGuard(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(k =>
        {
            k.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            k.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(p =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"] ?? string.Empty);
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["AppSettings:Secret"],
                ValidAudience = configuration["AppSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }
}