using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotDeskAPI.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DeskId { get; set; }
        public virtual Desk Desk { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
