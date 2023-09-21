using MediatR;
using Microsoft.AspNetCore.Mvc;
using sag.Application.Features.Auth.Models;
using sag.Application.Features.Auth.Queries;

namespace sag.api.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator) => _mediator = mediator;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login) =>
        Ok(await _mediator.Send(new LoginUserQuery(login)));
}