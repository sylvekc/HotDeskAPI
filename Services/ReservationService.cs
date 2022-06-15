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
    public interface IReservationService
    {
        int AddReservation(AddReservationDto dto);
    }

    public class ReservationService : IReservationService
    {
        private readonly HotDeskDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ReservationService(HotDeskDbContext dbContext, IMapper mapper, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public int AddReservation(AddReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            var deskId = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == dto.DeskNumber).Id;
            reservation.DeskLocation = dto.LocationName.ToUpper();
            reservation.DeskId = deskId;
            reservation.From = dto.From;
            reservation.To = dto.To;
            reservation.CreatedAt = DateTime.Now;
            reservation.UserId = (int)_userContextService.GetUserId;
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
            return reservation.Id;
        }
    }
}
