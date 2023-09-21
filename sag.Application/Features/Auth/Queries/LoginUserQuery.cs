using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Auth.Models;

namespace sag.Application.Features.Auth.Queries;

public record LoginUserQuery(LoginRequest LoginData): IRequest<Response<object>>;