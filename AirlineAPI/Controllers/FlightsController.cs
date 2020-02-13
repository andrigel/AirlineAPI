using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace AirlineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}