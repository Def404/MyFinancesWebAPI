using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Классификатор платежных систем, содержащий в себе название и изображение
/// </summary>
public partial class PaymentSystem
{
    public int PaymentSystemId { get; set; }

    public string Name { get; set; } = null!;

    public string ImagePath { get; set; } = null!;
    
}
