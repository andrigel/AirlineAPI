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
using ViewModels;

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
        public async Task<IActionResult> ReserveTicket(Guid flightId, TicketClass ticketClass, int PremiumMarksUsedCount)
        {
            var userId = User.Claims.ToList().Where(u => u.Type == "UserId").Single().Value;
            return Ok(await _taskRep.ReserveTicket(userId, flightId, ticketClass, PremiumMarksUsedCount));
        }

        [Authorize]
        [HttpPost("ReturnTicket")]
        public async Task<IActionResult> ReturnTicket(Guid ticketId)
        {
            await _taskRep.TryReturnTicket(ticketId);

            return Ok();
        }

        [Authorize]
        [HttpGet("GetKilometersInAir")]
        public async Task<IActionResult> GetKilometersInAir()
        {
            return Ok( await _taskRep.GetKilometersInAir(User.Claims.ToList()[1].Value));
        }

        [HttpPost("GetAdvice")]
        public List<FlightModel> GetAdvice(AdviceViewModel model)
        {
            return _taskRep.GetAdvice(model.From, model.To);
        }

        [HttpGet("GetValidFlights")]
        public List<FlightModel> GetValidFlights()
        {
            return _taskRep.GetValidFlights();
        }
    }
}