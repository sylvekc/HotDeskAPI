﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int LocationID { get; set; }
        public virtual Location Location { get; set; }

    }
}
