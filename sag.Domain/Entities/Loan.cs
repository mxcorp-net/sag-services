using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class Loan : BaseAuditableEntity
{
    [Required] public string Name { get; set; }
    [Required] public decimal OriginalAmount { get; set; }
    [Required] public Guid InstitutionId { get; set; }
    [Required] public PaymentCycle PaymentCycle { get; set; } = PaymentCycle.Monthly;
    [Required] public int Cycles { get; set; } = 1;

    [ForeignKey("Id")] public Institution Institution { get; set; }
}