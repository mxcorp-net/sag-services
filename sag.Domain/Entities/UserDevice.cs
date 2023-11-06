using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class UserDevice : BaseAuditableEntity
{
    [Required] public Guid UserId { get; set; }
    [Required] public string MacAddress { get; set; } = string.Empty;
    [Required] public string Token { get; set; } = string.Empty;
    [Required] public string OS { get; set; } = string.Empty;
    [Required] public EntityStatus Status { get; set; } = EntityStatus.Enable;
    
    [ForeignKey("UserId")] public User User { get; set; }
    
    
}