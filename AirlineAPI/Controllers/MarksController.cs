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
        public IActionResult GetMarksFromCurrentUser()
        {
            return Ok(_markRep.GetMarksFromUser(User.Claims.ToList()
                        .Where(c => c.Type == "UserId").Single().Value));
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetMarksFromUser")]
        public IActionResult GetMarksFromUser(string userId)
        {
            return Ok(_markRep.GetMarksFromUser(userId));
        }

        [Authorize]
        [HttpPost("AddMark")]
        public async Task<IActionResult> AddMark(Guid flightId)
        {
            var userId = User.Claims.ToList()
                .Where(c => c.Type == "UserId").Single().Value;

            await _markRep.AddMark(userId, flightId);
            return Ok();
        }

        [Authorize]
        [HttpPost("AddMarksMany")]
        public async Task<IActionResult> AddMarksMany(List<Guid> flightId)
        {
            var userId = User.Claims.ToList()
                .Where(c => c.Type == "UserId").Single().Value;

            await _markRep.AddMarksMany(userId, flightId);
            return Ok();
        }

        [Authorize]
        [HttpDelete("DeleteMark")]
        public async Task<IActionResult> DeleteMark(Guid markId)
        {
            await _markRep.DeleteMark(markId);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("DeleteMarksMany")]
        public async Task<IActionResult> DeleteMarksMany(List<Guid> markIds)
        {
            _markRep.DeleteMarksMany(markIds);
            return NoContent();
        }
    }
}