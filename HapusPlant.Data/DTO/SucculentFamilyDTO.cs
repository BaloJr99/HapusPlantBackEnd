using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HapusPlant.Data.DTO;

public partial class SucculentFamilyDTO
{
    public Guid IdSucculentFamily { get; set; }

    [Required]

    public string Family { get; set; } = null!;

    public Guid IdUser { get; set; }

}
