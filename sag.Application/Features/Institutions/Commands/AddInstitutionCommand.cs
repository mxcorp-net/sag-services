using MediatR;
using sag.Application.Common.Structs;
using sag.Application.Features.Institutions.Models;
using sag.Domain.Entities;

namespace sag.Application.Features.Institutions.Commands;

public record AddInstitutionCommand(InstitutionRequest Institution) : IRequest<Response<Institution>>;