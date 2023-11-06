using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    [Required] public TransactionType Type { get; set; }
    [Required] public Guid UserAccountId { get; set; }
    [Required] public decimal Amount { get; set; }

    [ForeignKey("UserAccountId")] public UserAccount UserAccount { get; set; }
    public ICollection<TransactionDetail> Details { get; } = new List<TransactionDetail>();
}