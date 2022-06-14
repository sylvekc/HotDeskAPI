﻿namespace HotDeskAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public List<Reservation> Reservations { get; set; }


    }
}
