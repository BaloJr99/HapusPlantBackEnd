using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HapusPlant.Data.DTO;

public partial class PersonalDatumDTO
{
    public Guid IdPersonalData { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string? Photo { get; set; }

    [Required]
    public DateOnly? Birthday { get; set; }
}
