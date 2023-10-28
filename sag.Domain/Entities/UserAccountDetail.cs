using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;

namespace sag.Domain.Entities;

public class UserAccountDetail : BaseAuditableEntity
{
    [Required] public Guid UserAccountId { get; set; }
    [Required] public string DetailName { get; set; }
    [Required] public string DetailValue { get; set; }
    
    [ForeignKey("UserAccountId")] public UserAccount UserAccount { get; set; }
}