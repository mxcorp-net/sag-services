using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Institutions.Models;
using sag.Domain.Entities;

namespace sag.Application.Features.Institutions.Queries;

public record GetInstitutionsQuery(InstitutionFilters Filters) : IRequest<Response<IEnumerable<Institution>>>;