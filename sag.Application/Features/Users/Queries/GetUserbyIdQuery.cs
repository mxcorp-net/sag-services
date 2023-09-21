using MediatR;
using sag.Domain.Entities;

namespace sag.Application.Features.Users.Queries;

public abstract record GetUserByIdQuery(Guid UserId): IRequest<User>;