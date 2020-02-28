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
    public class EFTaskRepository : ITaskRepository
    {
        private readonly EFDBContext _context;
        private readonly IMapper _mapper;

        public EFTaskRepository(EFDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<FlightModel> GetAdvice(string from, string to)
        {
            var flights = _context.Flights.Where(f => ((f.Start > DateTime.Now) && (f.From == from) && (f.To == to))).AsEnumerable();

            return (flights.Select(f => _mapper.Map<FlightModel>(f))).ToList();
        }

        public List<FlightModel> GetFlightsByDate(DateTime from, DateTime to)
        {
            var flights = _context.Flights.Where(f => ((f.Start > from) && (f.Start < to))).ToList();

            return (flights.Select(f => _mapper.Map<FlightModel>(f))).ToList();
        }

        public List<FlightModel> GetFlightsToday()
        {
            var from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var to = from.AddDays(1);
            return GetFlightsByDate(from, to);
        }

        //спитати
        public async Task<int> GetKilometersInAir(string userId)
        {
            int result = 0;
            var user = await _context.ApplicationsUsers.Where(u => u.Id == userId)
                .Include(u => u.Tickets).FirstOrDefaultAsync();

            if ((user == null) || (user.Tickets.Count == 0)) return result;

            foreach (var t in user.Tickets)
            {
                await _context.Entry(t).Reference(t => t.Flight).LoadAsync();
            }

            foreach (var t in user.Tickets)
            {
                result += t.Flight.Length;
            }
            return result;
        }

        public List<FlightModel> GetValidFlights()
        {
            var flights = _context.Flights.Where(f => f.Start > DateTime.Now).ToList();

            return (flights.Select(f => _mapper.Map<FlightModel>(f))).ToList();
        }

        public async Task<TicketModel> ReserveTicket(string userId, Guid flightId, TicketClass ticketClass = TicketClass.econom, int premiumMarksUsedCount = 0)
        {
            var flight = _context.Flights.Find(flightId);
            var user = _context.ApplicationsUsers.Find(userId);

            int endPrice = flight.Price;
            if (ticketClass == TicketClass.vip) endPrice = (int)(endPrice * 1.5f);

            if ((user.PremiumMarksCount < premiumMarksUsedCount) || (premiumMarksUsedCount > endPrice)) return null;

            endPrice -= premiumMarksUsedCount;
            user.PremiumMarksCount -= premiumMarksUsedCount;

            var ticket = new Ticket
            {
                FlightId = flightId,
                UserId = userId,
                PremiumMarksUsedCount = premiumMarksUsedCount,
                TicketClass = ticketClass,
                EndPrice = endPrice,
                IsBought = false
            };

            await _context.Tickets.AddAsync(ticket);
            flight.PlacesCount += 1;
            await _context.SaveChangesAsync();

            return _mapper.Map<TicketModel>(ticket);
        }

        public async Task TryReturnTicket(Guid ticketId)
        {
            var ticket = await _context.Tickets.Where(t => t.Id == ticketId).Include(t => t.User)
                .Include(t => t.Flight).SingleAsync();

            DateTime date = DateTime.Now;
            var date2 = date.AddDays(3);

            if ((ticket.Flight.Start > date2))
            {
                ticket.User.PremiumMarksCount += ticket.PremiumMarksUsedCount;
                ticket.Flight.PlacesReserved -= 1;

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}