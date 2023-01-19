using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность, описывающая банковскую карту
/// </summary>
public partial class DebitCard
{
    public int DebitCardId { get; set; }

    public DateOnly ActionDate { get; set; }

    public bool Visibility { get; set; }

    public string Login { get; set; } = null!;

    public int PaymentSystemId { get; set; }

    public long? BankAccountId { get; set; }

    public virtual BankAccount? BankAccount { get; set; }
    
    public virtual PaymentSystem? PaymentSystem { get; set; }
}
