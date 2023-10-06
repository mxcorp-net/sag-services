using sag.Domain.Common.Enums;

namespace sag.Application.Features.Institutions.Models;

public class InstitutionRequest
{
    public required string Name { get; set; }
    public required InstitutionType Type { get; set; }
    public required EntityStatus Status { get; set; } = EntityStatus.Enable;
}