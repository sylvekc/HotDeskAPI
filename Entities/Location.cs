using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public string Name { get; set; }
        public virtual List<Desk> Desks { get; set; }
    }
}
