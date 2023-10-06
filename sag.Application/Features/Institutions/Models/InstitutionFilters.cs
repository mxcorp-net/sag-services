using sag.Domain.Common.Enums;

namespace sag.Application.Features.Institutions.Models;

public class InstitutionFilters
{
    public InstitutionType? Type { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enable;
    public string? Text { get; set; }
}