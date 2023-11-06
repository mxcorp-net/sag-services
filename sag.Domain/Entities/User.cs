using System.ComponentModel.DataAnnotations;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class User : BaseAuditableEntity
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; }= string.Empty;
    public EntityStatus Status { get; set; } = EntityStatus.Enable;

    public ICollection<UserAccount> Accounts { get; set; } = new List<UserAccount>();
}