using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HapusPlant.Common
{
    public class ConnectionStrings
    {
        public string HapusPlantSql { get; set; } = null!;
        public string HapusPlantMongo { get; set; } = null!; 
    }

    public class Jwt
    {
        public string Key  { get; set; } = null!;
        public string Issuer  { get; set; } = null!;
        public string Audience  { get; set; } = null!;
    }
}