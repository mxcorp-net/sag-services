using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Auth.Models;

namespace sag.Application.Features.Auth.Queries;

public record CheckQuery(CheckRequest CheckRequest): IRequest<Response<bool>>;