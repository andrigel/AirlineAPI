﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class FlightsController : Controller
    {
        private readonly IFlightRepository _flightRep;

        public FlightsController(IFlightRepository flightRep)
        {
            _flightRep = flightRep;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            return Ok(await _flightRep.GetAll());
        }

        [HttpPut]
        public async Task<IActionResult> ModifyFlight(FlightModel flightModel)
        {
            await _flightRep.ModifyFlight(flightModel);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(FlightModel flightModel)
        {
            await _flightRep.AddFlight(flightModel);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            await _flightRep.DeleteFlight(id);
            return NoContent();
        }
    }
}