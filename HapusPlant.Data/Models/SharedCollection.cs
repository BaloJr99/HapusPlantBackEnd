using System;
using System.Collections.Generic;

namespace HapusPlant.Data.Models;

public partial class SharedCollection
{
    public Guid IdSharedCollection { get; set; }

    public Guid IdOriginalUser { get; set; }

    public Guid IdSharedUser { get; set; }

    public bool IsShared { get; set; }

    public virtual User IdOriginalUserNavigation { get; set; } = null!;

    public virtual User IdSharedUserNavigation { get; set; } = null!;
}
