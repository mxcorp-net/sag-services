using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;

namespace sag.Domain.Entities;

public class BankAccountDetail : BaseAuditableEntity
{
    public Guid BankAccountId { get; set; }
    [Required] public string DetailName { get; set; }
    [Required] public string DetailValue { get; set; }

    [ForeignKey("Id")] public BankAccount BankAccount { get; set; }
}