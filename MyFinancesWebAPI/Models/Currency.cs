using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность хранит наименования валют, их сокращенные названия и символы
/// </summary>
public partial class Currency
{
    public int CurrencyId { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public char Sign { get; set; }
    
}
