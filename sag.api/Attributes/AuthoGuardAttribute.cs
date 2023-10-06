using Microsoft.AspNetCore.Mvc.Filters;
using sag.Application.Exceptions;

namespace sag.api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthGuardAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        if (httpContext.Items.ContainsKey("Unauthorized"))
            throw new UnauthorizedException(
                (string)httpContext.Items.FirstOrDefault(i => (string)i.Key == "Unauthorized").Value!);

        if (!context.HttpContext.Items.ContainsKey("User"))
            throw new UnauthorizedException("Missing User context");
    }
}