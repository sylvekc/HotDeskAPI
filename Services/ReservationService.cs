using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotDeskAPI.Authorization;
using HotDeskAPI.Entities;
using HotDeskAPI.Exceptions;
using HotDeskAPI.Models;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IAuthorizationService _authorizationService;

        public ReservationService(HotDeskDbContext dbContext, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public int AddReservation(AddReservationDto dto)
        {
            var isImpossibleToAddReservation = _dbContext.Reservations.Where(x => x.DeskNumber == dto.DeskNumber).Any(
                x => x.From >= dto.From && x.From <= dto.To ||
                     x.To >= dto.From && x.To <= dto.To);
            var isImpossibleToAddReservation1 =
                _dbContext.Reservations.Where(x => x.DeskNumber == dto.DeskNumber).Any(x => x.From <= dto.From && x.To >= dto.To);

            if (isImpossibleToAddReservation || isImpossibleToAddReservation1)
            {
                throw new ForbidException("You can't add reservation. That desk is taken at this time.");
            }

            var lengthOfTheReservation = (dto.To - dto.From).Days;
            if (lengthOfTheReservation < 1)
            {
                throw new BadRequestException("You have to reserve the desk at least for 1 day.");
            }

            if (lengthOfTheReservation > 7)
            {
                throw new BadRequestException("You can reserve the desk max for 7 days.");
            }

            var reservation = _mapper.Map<Reservation>(dto);
            var deskId = _dbContext.Desks.FirstOrDefault(x => x.DeskNumber == dto.DeskNumber).Id;
            var availableDeskToReserve = _dbContext.Desks.Any(x => x.DeskNumber == dto.DeskNumber && x.Available);
            if (!availableDeskToReserve)
            {
                throw new ForbidException("This desk is unavailable.");
            }

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

            var impossibleToReserveNewDesk1 = _dbContext.Reservations.Where(x => x.DeskId == newDeskId).Any(x =>
                x.From >= reservation.From && x.From <= reservation.To ||
                x.To >= reservation.From && x.To <= reservation.To);
            var impossibleToReserveNewDesk2 = _dbContext.Reservations.Where(x => x.DeskId == newDeskId)
                .Any(x => x.From <= reservation.From && x.To >= reservation.To);

            if (impossibleToReserveNewDesk1 || impossibleToReserveNewDesk2)
            {
                throw new ForbidException("You can't reserve this desk. It is currently reserved at this time.");
            }

            var availableDeskToReserve = _dbContext.Desks.Any(x => x.DeskNumber == dto.DeskNumber && x.Available);
            if (!availableDeskToReserve)
            {
                throw new ForbidException("This desk is unavailable");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("You can't make changes at this reservation.");
            }

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
