using sag.Domain.Common.Enums;

namespace sag.Application.Features.Transactions.Models;

public class TransactionRequest
{
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Dictionary<TransactionKey, string> Details { get; set; } = new();
}