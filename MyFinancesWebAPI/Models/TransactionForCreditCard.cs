using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

public partial class TransactionForCreditCard
{
    public int? TransactionTypeId { get; set; }

    public string? Name { get; set; }

    public char? Class { get; set; }
}
