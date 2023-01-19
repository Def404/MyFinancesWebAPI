using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для кредита в банке
/// </summary>
public partial class Loan
{
    public long LoanId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly StartDate { get; set; }

    public short InterestRate { get; set; }

    public bool Visibility { get; set; }

    public string? Description { get; set; }

    public string Login { get; set; } = null!;

    public int BankId { get; set; }

    public int CurrencyId { get; set; }

    public int CreditPeriod { get; set; }

    public virtual Bank Bank { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual User LoginNavigation { get; set; } = null!;
}
