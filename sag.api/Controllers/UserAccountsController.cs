using MediatR;
using Microsoft.AspNetCore.Mvc;
using sag.api.Attributes;
using sag.Application.Features.UserAccounts.Commands;
using sag.Application.Features.UserAccounts.Models;
using sag.Application.Features.UserAccounts.Queries;
using sag.Domain.Entities;

namespace sag.api.Controllers;

// [AuthGuard]
[ApiController, Route("api/users/{userId:guid}/accounts")]
public class UserAccountsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserAccountsController(IMediator mediator) => _mediator = mediator;

    // TODO: complete the implementations
    
    // GET: api/users/{userId}/accounts
    [HttpGet]
    public async Task<IActionResult> GetUserAccounts(Guid userId) =>
        Ok(await _mediator.Send(new GetUserAccountsQuery(userId)));

    // GET: api/users/{userId}/accounts/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserAccountById(Guid id, Guid userId) => Ok();

    // POST: api/users/{userId}/accounts
    [HttpPost] //TODO: change userAccount parameter for a new object request type 
    public async Task<IActionResult> AddUserAccount([FromBody] UserAccountRequest request, Guid userId) => 
        Ok(await _mediator.Send(new AddUserAccountCommand(request, userId)));
     

    // PUT: api/Loans/5
    [HttpPut("{id:guid}")]
    public void Put(int id, [FromBody] string value, Guid userId)
    {
    }

    // DELETE: api/Loans/5
    [HttpDelete("{id:guid}")]
    public void Delete(int id, Guid userId)
    {
    }
}