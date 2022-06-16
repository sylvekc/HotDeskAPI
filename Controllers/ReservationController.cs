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

        [HttpPatch("{reservationId}")]
        public ActionResult ChangeDesk([FromRoute]int reservationId, [FromForm]ChangeDeskDto dto)
        {
            _reservationService.ChangeDesk(reservationId, dto);
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetReservationsForAdminDto>> GetReservations()
        {
            if (User.IsInRole("Admin"))
            {
                var reservationsDtos = _reservationService.GetReservationsForAdmin();
                return Ok(reservationsDtos);
            }

            if (User.IsInRole("Employee"))
            {
                var reservationsDtos = _reservationService.GetReservationsForEmployee();
                return Ok(reservationsDtos);
            }

            return BadRequest();
        }
    }
}
