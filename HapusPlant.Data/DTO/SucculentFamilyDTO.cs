using System;
using System.Collections.Generic;

namespace HapusPlant.Data.DTO;

public partial class SucculentFamilyDTO
{
    public Guid IdSucculentFamily { get; set; }

    public string Family { get; set; } = null!;

    public Guid IdUser { get; set; }

}
