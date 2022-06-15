using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Models
{
    public class AddLocationDto
    {
        public string Building { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
    }
}
