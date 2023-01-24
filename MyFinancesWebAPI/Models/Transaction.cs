using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность для транзакций осуществляющих пользователем
/// </summary>
public partial class Transaction
{
    public long TransactionId { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Description { get; set; }

    public string Login { get; set; } = null!;
    public string WalletName { get; set; }
    public char CurrencySign { get; set; }
    
    public virtual TransactionType? TransactionType { get; set; }
}
