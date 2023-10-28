using MediatR;
using sag.Application.Common.Structs;
using sag.Domain.Entities;

namespace sag.Application.Features.Users.Commands;

public record AddUserCommand(User User): IRequest<Response<User>>;
