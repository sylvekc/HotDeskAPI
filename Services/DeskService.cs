using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotDeskAPI.Entities;
using HotDeskAPI.Models;

namespace HotDeskAPI.Services
{
    public interface IDeskService
    {
        int AddDesk(AddDeskDto dto);
        bool DeleteDesk(int deskNumber, string locationName);
    }

    public class DeskService : IDeskService
    {
        private readonly HotDeskDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeskService(HotDeskDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int AddDesk(AddDeskDto dto)
        {
            var desk = _mapper.Map<Desk>(dto);
            int locationId = _dbContext.Locations.FirstOrDefault(x => x.Name == dto.LocationName).Id;
            desk.LocationId = locationId;
            desk.LocationName = dto.LocationName;
            desk.Description = dto.Description;
            desk.DeskNumber = dto.DeskNumber;
            desk.Available = true;
            _dbContext.Desks.Add(desk);
            _dbContext.SaveChanges();

            return desk.Id;
        }

        public bool DeleteDesk(int deskNumber, string locationName)
        {
            var desk = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == deskNumber && x.Location.Name == locationName);
            if (desk is null) return false;
            _dbContext.Desks.Remove(desk);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
