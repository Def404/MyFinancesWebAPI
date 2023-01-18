using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для счета в банке
/// </summary>
public partial class BankAccount
{
    public long? BankAccountId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }

    public bool Visibility { get; set; }

    public string? Description { get; set; }

    public string Login { get; set; } = null!;

    public int BankId { get; set; }

    public int CurrencyId { get; set; }

    public virtual Bank? Bank { get; set; } = null!;

    public virtual Currency? Currency { get; set; } = null!;
}
