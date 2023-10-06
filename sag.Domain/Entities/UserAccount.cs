using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class UserAccount : BaseAuditableEntity
{
    [Required] public string Name { get; set; }
    [Required] public Guid UserId { get; set; }
    [Required] public Guid InstitutionId { get; set; }
    [Required] public AccountType AccountType { get; set; }
    [Required] public EntityStatus Status { get; set; } = EntityStatus.Enable;
    
    [ForeignKey("Id")] public User User { get; set; }
    [ForeignKey("Id")] public Institution Institution { get; set; }
}