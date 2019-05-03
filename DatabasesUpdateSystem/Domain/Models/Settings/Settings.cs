﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Domain.Models.Settings
{
    public class Settings
    {
        public int ConfirmationExpires { get; set; }
        public AuthOptions AuthOptions { get; set; }
    }
}
