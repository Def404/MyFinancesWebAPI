using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для кредитной карты банка
/// </summary>
public partial class CreditCard
{
    public long CreditCardId { get; set; }

    public string Name { get; set; } = null!;

    public decimal CreditLimit { get; set; }

    public int FreePeriod { get; set; }

    public DateOnly ActionDate { get; set; }

    public bool Visibility { get; set; }

    public string? Description { get; set; }

    public string Login { get; set; } = null!;

    public int PaymentSystemId { get; set; }

    public int BankId { get; set; }

    public int CurrencyId { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual Currency? Currency { get; set; }
    
    public virtual PaymentSystem? PaymentSystem { get; set; }

    public virtual List<InterestRate>? InterestRates { get; set; }

}
