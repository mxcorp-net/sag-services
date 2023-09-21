using MediatR;
using sag.Domain.Entities;

namespace sag.Application.Features.Users.Commands;

public record AddUserCommand(User User): IRequest<User>;
