﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCode.Options
{
    public class JwtSettings
    {        
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
