using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using AutoMapper;
using AutoMapper.Models;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IUserRepository _userRep;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITaskRepository _taskRep;

        public TaskController(IUserRepository userRep, UserManager<ApplicationUser> userManager, ITaskRepository taskRepository)
        {
            _userRep = userRep;
            _userManager = userManager;
            _taskRep = taskRepository;
        }

        [HttpGet("GetFlightsToday")]
        public IActionResult GetFlightsToday()
        {
            return Ok(_taskRep.GetFlightsToday());
        }

        [HttpPost("GetFlightsByDate")]
        public IActionResult GetFlightsByDate(DateTime from, DateTime to)
        {
            
            return Ok(_taskRep.GetFlightsByDate(from, to));
        }

        [Authorize]
        [HttpPost("ReserveTicket")]
        public async Task<IActionResult> ReserveTicket(int flightId, TicketClass ticketClass, int PremiumMarksUsedCount)
        {
            ApplicationUser user;

            user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return BadRequest();
            Ticket ticket = await _taskRep.ReserveTicket(user.Id, flightId, ticketClass, PremiumMarksUsedCount);
            if (ticket == null) return NotFound();
            var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Ticket, TicketModel>());
            var mapper = new Mapper(configuration);
            if (ticket != null) return Ok(mapper.Map<Ticket, TicketModel>(ticket));
            return NotFound();
        }

        [Authorize]
        [HttpPost("ReturnTicket")]
        public async Task<IActionResult> ReturnTicket(int ticketId)
        {
            string userId = User.Claims.ToList()[1].Value;
            var u = await _userRep.GetUser(userId, true);
            if (u == null) return BadRequest();
            var ticket = u.Tickets.Where(t => t.Id == ticketId).FirstOrDefault();
            if(ticket == null) return BadRequest();
            return Ok(await _taskRep.TryReturnTicket(ticket.Id));
        }

        [HttpPost("GetKilometersInAir")]
        public async Task<IActionResult> GetKilometersInAir(string userId)
        {
            if ((userId == "0") && (User.Identity.IsAuthenticated)) userId = User.Claims.ToList()[1].Value;
            var u = await _userRep.GetUser(userId, true);
            if (User == null) return BadRequest();
            return Ok( await _taskRep.GetKilometersInAir(u.Id));
        }
    }
}