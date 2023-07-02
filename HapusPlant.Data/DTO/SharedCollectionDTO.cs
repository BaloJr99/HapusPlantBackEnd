using System;
using System.Collections.Generic;

namespace HapusPlant.Data.DTO;

public partial class SharedCollectionDTO
{
    public Guid IdOriginalUser { get; set; }

    public Guid IdSharedUser { get; set; }

    public bool IsShared { get; set; }

}

public partial class SharedUserDTO
{
    public Guid IdUser { get; set; }
    public string FullName { get; set; } = null!;
    public string? Photo { get; set; }

}
