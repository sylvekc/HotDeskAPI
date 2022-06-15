using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDeskAPI.Entities;

namespace HotDeskAPI.Models
{
    public class AddDeskDto
    {
        public int DeskNumber { get; set; }
        public string Description { get; set; }
        public string LocationName { get; set; }
    }
}
