using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;

namespace sag.Domain.Entities;

public class TransactionDetail : BaseAuditableEntity
{
    [Required] public Guid TransactionId { get; set; }
    [Required] public string DetailName { get; set; }
    [Required] public string DetailValue { get; set; }

    [ForeignKey("TransactionId")] public Transaction Transaction { get; set; }
}