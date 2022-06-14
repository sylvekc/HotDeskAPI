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
    [Route("api/desk")]
    [ApiController]
    [Authorize]

    public class DeskController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DeskController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        [HttpPost]
        public ActionResult AddDesk([FromForm] AddDeskDto dto)
        {
            var deskId = _deskService.AddDesk(dto);
            return Created($"/api/desk/{deskId}", null);
        }

    }
}
