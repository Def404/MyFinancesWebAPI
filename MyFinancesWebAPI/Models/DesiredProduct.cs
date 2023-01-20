using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для товара, который пользователь намерен приобрести в будущем
/// </summary>
public partial class DesiredProduct
{
    public long DesiredProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string? Link { get; set; }

    public string? ShopName { get; set; }

    public string Login { get; set; } = null!;

    public int CurrencyId { get; set; }

    public virtual Currency? Currency { get; set; } 
}
