using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotDeskAPI.Models;
using HotDeskAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskAPI.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public ActionResult AddReservation([FromForm]AddReservationDto dto)
        {
            var reservationId = _reservationService.AddReservation(dto);
            return Created($"/api/reservation/{reservationId}", null);
        }
    }
}
