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

        public TicketsController(ITicketRepository ticketRep)
        {
            _ticketRep = ticketRep;
        }

        [Authorize]
        [HttpGet("GetTicketsFromCurrentUser")]
        public async Task<IActionResult> GetTicketsFromCurrentUser()
        {
            return Ok(_ticketRep.GetTicketsFromUser(User.Claims.ToList()
                            .Where(c => c.Type == "UserId").Single().Value));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetTicketsFromUser")]
        public async Task<IActionResult> GetTicketsFromUser(string userId)
        {
            return Ok(_ticketRep.GetTicketsFromUser(userId));
        }
    }
}