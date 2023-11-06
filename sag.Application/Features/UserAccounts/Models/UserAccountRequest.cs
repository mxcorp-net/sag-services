using sag.Domain.Common.Enums;

namespace sag.Application.Features.UserAccounts.Models;

public class UserAccountRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid InstitutionId { get; set; }
    public AccountType AccountType { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enable;
}