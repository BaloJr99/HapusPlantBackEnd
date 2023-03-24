using System;
using System.Collections.Generic;

namespace HapusPlant.Data.Models;

public partial class User
{
    public Guid IdUser { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Jwt { get; set; }

    public string Role { get; set; } = null!;

    public Guid IdPersonalData { get; set; }

    public bool IsActive { get; set; }

    public virtual PersonalDatum IdPersonalDataNavigation { get; set; } = null!;

    public virtual ICollection<SucculentFamily> SucculentFamilies { get; } = new List<SucculentFamily>();

    public virtual ICollection<SucculentKind> SucculentKinds { get; } = new List<SucculentKind>();
}
