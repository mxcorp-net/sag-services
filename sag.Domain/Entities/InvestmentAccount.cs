using sag.Domain.Common;

namespace sag.Domain.Entities;

public class InvestmentAccount: BaseAuditableEntity
{
    public string Name { get; set; }
    public decimal Balance { get; set; } = 0;
}