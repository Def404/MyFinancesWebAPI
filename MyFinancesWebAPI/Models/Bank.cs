using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// В данном классификаторе содержится название банка и его цвет (для визуализации)
/// </summary>
public partial class Bank
{
    public int BankId { get; set; }

    public string Name { get; set; } = null!;

    public string Colour { get; set; } = null!;

    //public virtual ICollection<BankAccount> BankAccounts { get; } = new List<BankAccount>();
}
