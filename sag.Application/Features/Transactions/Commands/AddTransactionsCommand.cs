using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Transactions.Models;
using sag.Domain.Entities;

namespace sag.Application.Features.Transactions.Commands;

public record AddTransactionCommand(TransactionRequest TransactionRequest, Guid UserId, Guid UserAccountId) : IRequest<Response<Transaction>>;