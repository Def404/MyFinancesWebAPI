using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для подписок на различные сервисы (например, музыка)
/// </summary>
public partial class Subscription
{
    public long SubscriptionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string Login { get; set; } = null!;

    public long BankAccountId { get; set; }

    public virtual BankAccount BankAccount { get; set; } = null!;

    public virtual User LoginNavigation { get; set; } = null!;
}
