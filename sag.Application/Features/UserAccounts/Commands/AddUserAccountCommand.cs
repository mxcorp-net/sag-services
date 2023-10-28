using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.UserAccounts.Models;
using sag.Domain.Entities;

namespace sag.Application.Features.UserAccounts.Commands;

public record AddUserAccountCommand(UserAccountRequest UserAccount, Guid UserId) : IRequest<Response<UserAccount>>;