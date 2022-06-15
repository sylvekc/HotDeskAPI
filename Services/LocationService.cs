using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotDeskAPI.Entities;
using HotDeskAPI.Exceptions;
using HotDeskAPI.Models;

namespace HotDeskAPI.Services
{
    public interface ILocationService
    {
        int AddLocation(AddLocationDto dto);
        bool DeleteLocation(string name);
    }

    public class LocationService : ILocationService
    {
        private readonly HotDeskDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(HotDeskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddLocation(AddLocationDto dto)
        {
            var location = _mapper.Map<Location>(dto);
            var locationName = dto.Building + dto.Floor + dto.RoomNumber;
            location.Building = dto.Building;
            location.Floor = dto.Floor;
            location.RoomNumber = dto.RoomNumber;
            location.Name = locationName;
            var existLocation = _dbContext.Locations.Any(x => x.Name == locationName);
            if (existLocation)
            {
                throw new ForbidException("This location has already exist.");
            }

            _dbContext.Locations.Add(location);
            _dbContext.SaveChanges();
            return location.Id;
        }

        public bool DeleteLocation(string name)
        {
            var location = _dbContext.Locations.FirstOrDefault(x => x.Name == name.ToUpper());
            if (location is null)
            {
                throw new NotFoundException($"Location with name {name.ToUpper()} doesn't exist");
            }
            _dbContext.Locations.Remove(location);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
