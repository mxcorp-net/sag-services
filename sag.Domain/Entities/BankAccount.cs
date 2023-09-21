using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class BankAccount : BaseAuditableEntity
{
    [Required] public string Name { get; set; }
    public BankAccountType Type { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enable;
    public CurrencyType Currency { get; set; } = CurrencyType.MXN;
    public decimal Balance { get; set; } = 0;
    public Guid InstitutionId { get; set; }
    public Guid UserId { get; set; }

    [ForeignKey("Id")] public Institution Bank { get; set; }
    [ForeignKey("Id")] public User User { get; set; }
}