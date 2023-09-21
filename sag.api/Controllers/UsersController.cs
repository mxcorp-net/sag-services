using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sag.Application.Features.Users.Commands;
using sag.Application.Features.Users.Queries;
using sag.Domain.Entities;

namespace sag.api.Controllers;

[Authorize]
[ApiController, Route("api/users")]
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator) => _mediator = mediator;

    // Get a all Users
    [HttpGet]
    public async Task<IActionResult> GetUsers() =>
        Ok(await _mediator.Send(new GetUsersQuery()));

    /// <summary>
    /// Add new User
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user) =>
        Ok(await _mediator.Send(new AddUserCommand(user)));
}