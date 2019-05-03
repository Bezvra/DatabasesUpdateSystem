using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Domain.Models.Settings
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int AccessLifeTime { get; set; }
        public int RefreshLifeTime { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
