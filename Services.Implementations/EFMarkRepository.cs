using AutoMapper;
using AutoMapper.Models;
using DataLayer;
using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EFMarkRepository : IMarkRepository
    {
        private readonly EFDBContext _context;
        private readonly IMapper _mapper;
        public EFMarkRepository(EFDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddMark(string userId, Guid flightId)
        {
            var user = await _context.Users.FindAsync(userId);

            var flight = await _context.Flights.FindAsync(flightId);
            if ((user == null) || (flight == null))
                throw new Exception("User of flight not found");

            var userMark = new UserMark { FlightId = flight.Id, UserId = user.Id };
            await _context.UserMarks.AddAsync(userMark);
            await _context.SaveChangesAsync();
        }

        public async Task AddMark(ApplicationUser user, Guid flightId)
        {
            var userMark = new UserMark { FlightId = flightId, UserId = user.Id };
            await _context.UserMarks.AddAsync(userMark);
            await _context.SaveChangesAsync();
        }

        public async Task AddMarksMany(string userId, List<Guid> flightIds)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("User not found");
            try
            {
                flightIds.ForEach(id => AddMark(user, id)); // спитати 
            }
             catch
            {
                throw new Exception("Some Ids are not valid. Operation aborted");
            }
        }

        public async Task DeleteMark(Guid markId)
        {
            var mark = await _context.UserMarks.FindAsync(markId);
            if (mark == null)
                throw new Exception("Mark not found");
            _context.UserMarks.Remove(mark);
            await _context.SaveChangesAsync();
        }

        public void DeleteMarksMany(List<Guid> markIds)
        {
            try
            {
                markIds.ForEach(id => DeleteMark(id));
            }
            catch
            {
                throw new Exception("Some Ids are not valid. Operation aborted");
            }
        }

        public List<FlightModel> GetMarksFromUser(string userId)
        {
            var marks = _context.UserMarks.Where(m => m.UserId == userId).Include(m => m.Flight).AsEnumerable();

            return marks.Select(m => _mapper.Map<FlightModel>(m)).ToList();
        }
    }
}
