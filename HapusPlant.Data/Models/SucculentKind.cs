using System;
using System.Collections.Generic;

namespace HapusPlant.Data.Models;

public partial class SucculentKind
{
    public Guid IdSucculent { get; set; }

    public string Kind { get; set; } = null!;

    public bool IsEndemic { get; set; }

    public bool HasDocuments { get; set; }

    public string? DocumentsLink { get; set; }

    public bool IsAlive { get; set; }

    public Guid IdSucculentFamily { get; set; }

    public Guid IdUser { get; set; }

    public string PhotoLink { get; set; } = null!;

    public virtual SucculentFamily IdSucculentFamilyNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
