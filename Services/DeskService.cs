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

            desk.LocationId = dto.LocationId;
            desk.Description = dto.Description;
            desk.DeskNumber = dto.DeskNumber;
            desk.Available = dto.Available;
            _dbContext.Desks.Add(desk);
            _dbContext.SaveChanges();

            return desk.Id;
        }

    }
}
