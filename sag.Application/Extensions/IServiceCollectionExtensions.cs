using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using sag.Application.Middlewares;

namespace sag.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}