using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Данная сущность требуется для записи процентных ставок для кредита
/// </summary>
public partial class InterestRate
{
    public int InterestRateId { get; set; }

    public string Name { get; set; } = null!;

    public short Percent { get; set; }

    public string? Description { get; set; }

    public long CreditCardId { get; set; }

    public virtual CreditCard CreditCard { get; set; } = null!;
}
