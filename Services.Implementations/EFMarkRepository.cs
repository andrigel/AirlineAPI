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
        private readonly Mapper FlightMapper;
        public EFMarkRepository(EFDBContext context)
        {
            _context = context;

            var FlightConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightModel>());
            FlightMapper = new Mapper(FlightConfiguration);
        }

        public async Task<bool> AddMark(string userId, Guid flightId, ApplicationUser readyUser = null)
        {
            try
            {
                ApplicationUser user;
                if (readyUser == null) user = await _context.Users.FindAsync(userId);
                else user = readyUser;

                var flight = await _context.Flights.FindAsync(flightId);
                if ((user == null) || (flight == null)) return false;

                var userMark = new UserMark { FlightId = flight.Id, UserId = user.Id };
                await _context.UserMarks.AddAsync(userMark);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> AddMarksMany(string userId, List<Guid> flightIds)
        {
            int countAdded = 0;
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return 0;

            foreach (var flightId in flightIds)
            {
                if (await AddMark("", flightId, user)) countAdded++;
            }
            return countAdded;
        }

        public async Task<bool> DeleteMark(string userId, Guid flightId, ApplicationUser readyUser = null)
        {
            ApplicationUser user;
            if (readyUser == null) user = await _context.Users.Where(u => u.Id == userId).Include(u => u.UserMarks).FirstOrDefaultAsync();
            else user = readyUser;

            var flight = await _context.Flights.FindAsync(flightId);
            if ((user == null) || (flight == null)) return false;

            try
            {
                foreach (var um in user.UserMarks)
                {
                    await _context.Entry(um).Reference(um => um.Flight).LoadAsync();
                }
                foreach (var um in user.UserMarks)
                {
                    if (um.Flight.Id == flightId)
                    {
                        _context.Flights.Remove(flight);
                        await _context.SaveChangesAsync();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> DeleteMarksMany(string userId, List<Guid> flightIds)
        {
            int countDeleted = 0;
            var user = await _context.Users.Where(u => u.Id == userId).Include(u => u.UserMarks).FirstOrDefaultAsync();
            if (user == null) return 0;
            foreach (var id in flightIds)
            {
                if (await DeleteMark("", id, user)) countDeleted++;
            }
            return countDeleted;
        }

        public async Task<List<FlightModel>> GetMarksFromUser(string userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).Include(u => u.UserMarks).FirstOrDefaultAsync();
            List<FlightModel> fm = new List<FlightModel>();
            if (user == null) return fm;

            foreach (var mark in user.UserMarks)
            {
                await _context.Entry(mark).Reference(m => m.Flight).LoadAsync();
            }
            foreach (var mark in user.UserMarks)
            {
                fm.Add(FlightMapper.Map<FlightModel>(mark.Flight));
            }
            return fm;
        }
    }
}
