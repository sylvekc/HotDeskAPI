using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDeskAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotDeskAPI
{
    public class Seeder
    {
        private readonly HotDeskDbContext _dbContext;
        public Seeder(HotDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Locations.Any())
                {
                    var locations = GetLocations();
                    _dbContext.Locations.AddRange(locations);
                    _dbContext.SaveChanges();
                }

            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "Admin"
                    },

                    new Role()
                    {
                        Name = "Employee"
                    },
                };

            return roles;
        }

        private IEnumerable<Location> GetLocations()
        {
            var locations = new List<Location>()
                {
                    new Location()
                    {
                        Building = "A",
                        Floor = 1,
                        RoomNumber = 1,
                        Name = "A11"
                    },
                    new Location()
                    {
                        Building = "A",
                        Floor = 1,
                        RoomNumber = 2,
                        Name ="A12"
                    },
                    new Location()
                    {
                        Building = "A",
                        Floor = 1,
                        RoomNumber = 3, 
                        Name = "A13"
                    },
                    new Location()
                    {
                        Building = "B",
                        Floor = 3,
                        RoomNumber = 10,
                        Name = "B310"
                    },
                    new Location()
                    {
                        Building = "B",
                        Floor = 3,
                        RoomNumber = 11,
                        Name ="B311",
                    },
                    new Location()
                    {
                        Building = "B",
                        Floor = 3,
                        RoomNumber = 12,
                        Name = "B312"
                    },
                };

            return locations;
        }

    }
}
