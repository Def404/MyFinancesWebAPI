using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность хранилища, предназначенное для наличных денег, например, кошелек, копилка и т.п.
/// </summary>
public partial class LocalStorage
{
    public long LocalStorageId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Balance { get; set; }

    public string? Description { get; set; }

    public bool Visibility { get; set; }

    public string Login { get; set; } = null!;

    public int LocalStorageClassifierId { get; set; }

    public int CurrencyId { get; set; }

    public virtual Currency Currency { get; set; } = null!;

    public virtual LocalStorageClassifier LocalStorageClassifier { get; set; } = null!;

    public virtual User LoginNavigation { get; set; } = null!;
    
}
