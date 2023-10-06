using MediatR;
using sag.Application.Common.Structs;
using sag.Domain.Entities;

namespace sag.Application.Features.UserAccounts.Queries;

public record GetUserAccountsQuery(Guid UserId) : IRequest<Response<IEnumerable<UserAccount>>>;