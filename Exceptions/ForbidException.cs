﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message)
        {
            
        }
    }
}
