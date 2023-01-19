using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// В данном классификаторе, содержится информация для визуализации приложения, такая как, название и номер иконки выбранного хранилища
/// </summary>
public partial class LocalStorageClassifier
{
    public int LocalStorageClassifierId { get; set; }

    public string Name { get; set; } = null!;

    public int PictureNumber { get; set; }
    
}
