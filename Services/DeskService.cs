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
    public interface IDeskService
    {
        int AddDesk(AddDeskDto dto);
        bool DeleteDesk(int deskNumber, string locationName);
        bool ChangeAvailability(int deskNumber);
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
            desk.LocationName = dto.LocationName.ToUpper();
            desk.Description = dto.Description;
            desk.DeskNumber = dto.DeskNumber;
            desk.Available = true;
            _dbContext.Desks.Add(desk);
            _dbContext.SaveChanges();

            return desk.Id;
        }

        public bool DeleteDesk(int deskNumber, string locationName)
        {
            var reservedDesk = _dbContext.Reservations.Any(x => x.DeskNumber == deskNumber && x.To > DateTime.Now);
            var desk = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == deskNumber && x.Location.Name == locationName.ToUpper());
            if (desk is null)
            {
                throw new NotFoundException($"Desk with number {deskNumber} in {locationName.ToUpper()} doesn't exist.");
            }

            if (reservedDesk)
            {
                throw new ForbidException("You can't delete this desk, because it is reserved");
            }

            _dbContext.Desks.Remove(desk);
            _dbContext.SaveChanges();
            return true;
        }

        public bool ChangeAvailability(int deskNumber)
        {
            var desk = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == deskNumber);
            if (desk is null)
            {
                throw new NotFoundException($"Desk with number {deskNumber} doesn't exist.");
            }

            if (desk.Available)
            {
                desk.Available = false;
            }
            else
            {
                desk.Available = true;
            }

            _dbContext.SaveChanges();
            return true;
        }
    }

}

