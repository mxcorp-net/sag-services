using MediatR;
using sag.Domain.Entities;

namespace sag.Application.Features.Users.Queries;

public record GetUsersQuery: IRequest<IEnumerable<User>>;