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
    public interface IReservationService
    {
        int AddReservation(AddReservationDto dto);
        bool ChangeDesk(int reservationId, ChangeDeskDto dto);
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


        public bool ChangeDesk(int reservationId, ChangeDeskDto dto)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(x => x.Id == reservationId);
            var newDeskId = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == dto.DeskNumber).Id;
            if (reservation is null)
            {
                throw new NotFoundException($"Reservation with ID: {reservation} doesn't exist.");
            }

            if (reservation.From < DateTime.Now.AddDays(1))
            {
                throw new ForbidException("You can change the desk at least 24h before reservation starts.");
            }

            reservation.DeskNumber = dto.DeskNumber;
            reservation.DeskLocation = dto.DeskLocation;
            reservation.CreatedAt = DateTime.Now;
            reservation.DeskId = newDeskId;
            _dbContext.SaveChanges();
            return true;
        }

    }
}
