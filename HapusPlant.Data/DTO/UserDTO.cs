using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HapusPlant.Data.DTO;

public partial class UserDTO
{
    public Guid IdUser { get; set; }

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public string? Jwt { get; set; }

    public string? Role { get; set; }

    public Guid IdPersonalData { get; set; }

    public bool IsActive { get; set; }

}
