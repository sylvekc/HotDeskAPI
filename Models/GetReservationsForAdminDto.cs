using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDeskAPI.Entities;

namespace HotDeskAPI.Models
{
    public class GetReservationsForAdminDto
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DeskId { get; set; }
        public int UserId { get; set; }
        public int DeskNumber { get; set; }
        public string DeskLocation { get; set; }
    }
}
