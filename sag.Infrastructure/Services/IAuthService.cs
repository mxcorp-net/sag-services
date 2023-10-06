using sag.Domain.Entities;

namespace sag.Infrastructure.Services;

public interface IAuthService
{
    public User User { get; set; }
}