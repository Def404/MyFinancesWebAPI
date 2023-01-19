using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Классификатор для определения вида транзакции (перевод, платеж, пополнение и т.п.)
/// </summary>
public partial class TransactionType
{
    public int TransactionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public char Class { get; set; }
}
