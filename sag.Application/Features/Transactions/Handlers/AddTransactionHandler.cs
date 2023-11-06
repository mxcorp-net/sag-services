using MediatR;
using Microsoft.EntityFrameworkCore;
using sag.Application.Common.Structs;
using sag.Application.Features.Transactions.Commands;
using sag.Application.Features.Transactions.Models;
using sag.Domain.Common.Enums;
using sag.Domain.Entities;
using sag.Persistence.Contexts;

namespace sag.Application.Features.Transactions.Handlers;

public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, Response<Transaction>>
{
    private readonly SagDbContext _sagDbContext;

    public AddTransactionHandler(SagDbContext sagDbContext) => _sagDbContext = sagDbContext;

    public async Task<Response<Transaction>> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var newTran = request.TransactionRequest.Type switch
        {
            TransactionType.Inbound => await InboundAction(request, cancellationToken),
            TransactionType.Outbound => await OutboundAction(request, cancellationToken),
            _ => throw new ArgumentOutOfRangeException()
        };

        await _sagDbContext.SaveChangesAsync(cancellationToken);
        return Response<Transaction>.Success(newTran);
    }

    private async Task<Transaction> OutboundAction(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var account = await GetUserAccount(request.UserAccountId, cancellationToken);
        account.Balance -= request.TransactionRequest.Amount;
        
        var transaction = new Transaction
        {
            Type = request.TransactionRequest.Type,
            UserAccountId = request.UserAccountId,
            Amount = request.TransactionRequest.Amount,
            CreatedAt = request.TransactionRequest.CreatedAt
        };
        
        var newTrans = await _sagDbContext.Transactions.AddAsync(transaction, cancellationToken);
        await AddTransactionDetails(newTrans.Entity.Id, request.TransactionRequest.Details, cancellationToken);

        if (request.TransactionRequest.Details.ContainsKey(TransactionKey.DestinationAccountId))
        {
            var nRequest = request with
            {
                TransactionRequest = new TransactionRequest
                {
                    Type = TransactionType.Inbound,
                    Amount = request.TransactionRequest.Amount,
                    CreatedAt = request.TransactionRequest.CreatedAt
                },
                UserId = request.UserId,
                UserAccountId = new Guid(request.TransactionRequest.Details.Single(k => k.Key == TransactionKey.DestinationAccountId).Value)
            };
            await InboundAction(nRequest, cancellationToken);
        }
        
        return newTrans.Entity;
    }

    private async Task<Transaction> InboundAction(AddTransactionCommand request, CancellationToken cancellationToken)
    {
        var account = await GetUserAccount(request.UserAccountId, cancellationToken);
        account.Balance += request.TransactionRequest.Amount;
        
        var transaction = new Transaction
        {
            Type = request.TransactionRequest.Type,
            UserAccountId = request.UserAccountId,
            Amount = request.TransactionRequest.Amount,
            CreatedAt = request.TransactionRequest.CreatedAt
        };
        
        var newTrans = await _sagDbContext.Transactions.AddAsync(transaction, cancellationToken);
        await AddTransactionDetails(newTrans.Entity.Id, request.TransactionRequest.Details, cancellationToken);
        // await _sagDbContext.TransactionDetails.AddRangeAsync(AddTrasactionDetails(newTrans.Entity.Id,
        //     request.TransactionRequest.Details), cancellationToken);
        return newTrans.Entity;
    }

    private async Task AddTransactionDetails(Guid transactionId, Dictionary<TransactionKey, string> transactionRequestDetails, CancellationToken cancellationToken)
    {
        foreach (var detail in transactionRequestDetails)
        {
            var newDetail = new TransactionDetail
            {
                TransactionId = transactionId,
                Key = detail.Key,
                Value = detail.Value
            };

            await _sagDbContext.TransactionDetails.AddAsync(newDetail, cancellationToken);
        }
    }

    private async Task<UserAccount> GetUserAccount(Guid userAccountId, CancellationToken cancellationToken)
        => await _sagDbContext.UserAccounts.SingleAsync(a => a.Id == userAccountId, cancellationToken);
}