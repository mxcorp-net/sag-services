using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class UserAccount : BaseAuditableEntity
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public Guid UserId { get; set; }
    [Required] public Guid InstitutionId { get; set; }
    [Required] public AccountType AccountType { get; set; }
    [Required] public decimal Balance { get; set; }
    [Required] public EntityStatus Status { get; set; } = EntityStatus.Enable;


    [ForeignKey("UserId")] public User User { get; set; }
    [ForeignKey("InstitutionId")] public Institution Institution { get; set; }
}