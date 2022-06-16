using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskAPI.Models
{
    public class GetReservationsForEmpleyeeDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int DeskNumber { get; set; }
        public string DeskLocation { get; set; }
    }
}
