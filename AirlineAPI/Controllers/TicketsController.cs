using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _ticketRep;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketsController(ITicketRepository ticketRep, UserManager<ApplicationUser> userManager)
        {
            _ticketRep = ticketRep;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetTicketsFromCurrentUser")]
        public async Task<IActionResult> GetTicketsFromCurrentUser()
        {
            var userId = User.Claims.ToList()[1].Value;
            var u = await _userManager.FindByIdAsync(userId);
            if (u == null) return BadRequest();

            return Ok(await _ticketRep.GetTicketsFromUser(userId));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetTicketsFromUser")]
        public async Task<IActionResult> GetTicketsFromUser(string userId)
        {
                if ((userId == "0") && (User.Identity.IsAuthenticated)) userId = User.Claims.ToList()[1].Value;
                var u = await _userManager.FindByIdAsync(userId);
                if (u == null) return BadRequest();

            return Ok(await _ticketRep.GetTicketsFromUser(userId));
        }
    }
}