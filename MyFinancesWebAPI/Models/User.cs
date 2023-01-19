using System;
using System.Collections.Generic;

namespace MyFinancesWebAPI.Models;

/// <summary>
/// Сущность содержит в себе информацию о пользователе приложения
/// </summary>
public partial class User
{
    public string Login { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public DateOnly RegistrationDate { get; set; }
}
