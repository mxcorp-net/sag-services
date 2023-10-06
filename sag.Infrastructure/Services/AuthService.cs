using Microsoft.AspNetCore.Http;
using sag.Domain.Entities;

namespace sag.Infrastructure.Services;

// TODO: Put here JWT helpers
public class AuthService : IAuthService
{
    
    public User User { get; set; } = new();

    public AuthService(IHttpContextAccessor contextAccessor)
    {
        var context = contextAccessor.HttpContext ?? new DefaultHttpContext();

        if (context.Items.ContainsKey("User"))
        {
            User = (User)(context.Items.First(i => (string)i.Key == "User").Value ?? new User());
        }
    }
}