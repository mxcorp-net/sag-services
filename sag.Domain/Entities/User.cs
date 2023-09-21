using System.ComponentModel.DataAnnotations;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class User : BaseAuditableEntity
{
    [Required] public string Name { get; set; }
    [Required] public string Email { get; set; } 
    [Required] public string Password { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enable;
}