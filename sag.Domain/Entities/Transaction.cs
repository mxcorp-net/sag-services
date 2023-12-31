﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using sag.Domain.Common;
using sag.Domain.Common.Enums;

namespace sag.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    [Required] public TransactionType Type { get; set; }
    [Required] public Guid BackAccountId { get; set; }
    [Required] public decimal Amount { get; set; }

    [ForeignKey("Id")] public BankAccount BankAccount { get; set; }
}