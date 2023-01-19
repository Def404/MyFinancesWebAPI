using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

public partial class Deposit
{
    public long DepositId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Amount { get; set; }

    public int Period { get; set; }

    public short InterestRate { get; set; }

    public bool Visibility { get; set; }

    public string? Description { get; set; }

    public string Login { get; set; } = null!;

    public int BankId { get; set; }

    public int CurrencyId { get; set; }

    public virtual Bank Bank { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual User LoginNavigation { get; set; } = null!;

}
