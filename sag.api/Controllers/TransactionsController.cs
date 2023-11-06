using MediatR;
using Microsoft.AspNetCore.Mvc;
using sag.api.Attributes;
using sag.Application.Features.Transactions.Commands;
using sag.Application.Features.Transactions.Models;

namespace sag.api.Controllers;

[AuthGuard]
[ApiController, Route("api/users/{userId:guid}/accounts/{accountId:guid}/transactions")]
public class TransactionsController : Controller
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Add new transaction
    /// </summary>
    /// <param name="transactionRequest"></param>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddTransaction([FromBody] TransactionRequest transactionRequest, Guid userId, Guid accountId) => 
        Ok(await _mediator.Send(new AddTransactionCommand(transactionRequest, userId, accountId)));
}