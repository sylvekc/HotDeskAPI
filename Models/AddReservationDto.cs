using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDeskAPI.Entities;

namespace HotDeskAPI.Models
{
    public class AddReservationDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string LocationName { get; set; }
        public int DeskNumber { get; set; }
    }
}
