using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HapusPlant.Data.DTO
{
    public class NewUserDTO
    {
        public PersonalDatumDTO personalDatum { get; set ;} = null!;
        public UserDTO user { get; set ;} = null!;
    }
}