using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        public int DeskNumber { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public bool Available { get; set; }
        public virtual Location Location { get; set; }

    }
}
