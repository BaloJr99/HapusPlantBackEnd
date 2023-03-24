using System;
using System.Collections.Generic;

namespace HapusPlant.Data.Models;

public partial class SucculentFamily
{
    public Guid IdSucculentFamily { get; set; }

    public string Family { get; set; } = null!;

    public Guid IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<SucculentKind> SucculentKinds { get; } = new List<SucculentKind>();
}
