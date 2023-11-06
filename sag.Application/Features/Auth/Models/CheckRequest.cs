namespace sag.Application.Features.Auth.Models;

public class CheckRequest
{
    public Guid DeviceId { get; set; }
    public string DeviceToken { get; set; } = string.Empty;
}