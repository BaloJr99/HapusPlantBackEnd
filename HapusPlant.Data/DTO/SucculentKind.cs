using System;
using System.Collections.Generic;

namespace HapusPlant.Data.DTO;

public partial class SucculentKindDTO
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

}
