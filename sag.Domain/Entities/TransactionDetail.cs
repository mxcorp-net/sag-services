using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class TransactionDetail : BaseAuditableEntity
{
    [Required] public Guid TransactionId { get; set; }
    [Required] public TransactionKey Key { get; set; }
    [Required] public string Value { get; set; } = string.Empty;

    [ForeignKey("TransactionId")] public Transaction Transaction { get; set; } = null!;
}