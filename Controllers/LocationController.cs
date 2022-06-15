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
    [Route("api/location")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public ActionResult AddLocation([FromForm] AddLocationDto dto)
        {
            var locationId = _locationService.AddLocation(dto);
            return Created($"/api/location/{locationId}", null);
        }

    }
}
