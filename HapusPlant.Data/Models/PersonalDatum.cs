using System;
using System.Collections.Generic;

namespace HapusPlant.Data.Models;

public partial class PersonalDatum
{
    public Guid IdPersonalData { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    public DateOnly Birthday { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
