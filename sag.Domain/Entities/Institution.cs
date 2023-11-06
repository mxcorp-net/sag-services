using System.ComponentModel.DataAnnotations;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class Institution : BaseAuditableEntity
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public InstitutionType Type { get; set; }
    [Required] public EntityStatus Status { get; set; } = EntityStatus.Enable;

    public ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}