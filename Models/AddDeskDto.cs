using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Models
{
    public class AddDeskDto
    {
        public int DeskNumber { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public bool Available { get; set; } = true;
    }
}
