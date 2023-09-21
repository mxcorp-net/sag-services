namespace sag.Application.Features.Auth.Models;

public struct LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool? RememberMe { get; set; }
}