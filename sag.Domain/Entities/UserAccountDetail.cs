using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using sag.Domain.Common;

namespace sag.Domain.Entities;

public class UserAccountDetail : BaseAuditableEntity
{
    [Required] public Guid UserAccountId { get; set; }
    [Required] public string DetailLabel { get; set; } = string.Empty;
    [Required] public string DetailKey { get; set; } = string.Empty;
    [Required] public string DetailValue { get; set; } = string.Empty;

    [ForeignKey("UserAccountId")] public UserAccount UserAccount { get; set; }
}