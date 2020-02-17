using System;
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
    public class MarkController : Controller
    {
        private readonly IMarkRepository _markRep;

        public MarkController(IMarkRepository markRep)
        {
            _markRep = markRep;
        }

        [Authorize]
        [HttpGet("GetMarksFromCurrentUser")]
        public async Task<IActionResult> GetMarksFromCurrentUser()
        {
            return Ok(await _markRep.GetMarksFromUser(User.Claims.ToList()[1].Value));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetMarksFromUser")]
        public async Task<IActionResult> GetMarksFromUser(string userId)
        {
            if ((userId == "0") && (User.Identity.IsAuthenticated)) userId = User.Claims.ToList()[1].Value;
            return Ok(await _markRep.GetMarksFromUser(userId));
        }

        [Authorize]
        [HttpPost("AddMark")]
        public async Task<IActionResult> AddMark(Guid flightId)
        {
            var userId = User.Claims.ToList()[1].Value;
            return Ok(await _markRep.AddMark(userId,flightId));
        }

        [Authorize]
        [HttpPost("AddMarksMany")]
        public async Task<IActionResult> AddMarksMany(List<Guid> flightId)
        {
            var userId = User.Claims.ToList()[1].Value;
            return Ok(await _markRep.AddMarksMany(userId, flightId));
        }

        [Authorize]
        [HttpDelete("DeleteMark")]
        public async Task<IActionResult> DeleteMark(Guid flightId)
        {
            var userId = User.Claims.ToList()[1].Value;
            return Ok(await _markRep.DeleteMark(userId, flightId));
        }

        [Authorize]
        [HttpDelete("DeleteMarksMany")]
        public async Task<IActionResult> DeleteMarksMany(List<Guid> flightId)
        {
            var userId = User.Claims.ToList()[1].Value;
            return Ok(await _markRep.DeleteMarksMany(userId, flightId));
        }
    }
}